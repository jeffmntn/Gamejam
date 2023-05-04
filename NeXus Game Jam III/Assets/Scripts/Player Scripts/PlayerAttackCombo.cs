using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ComboState
{
    None,
    Attack1,
    Attack2,
    Attack3
}
public class PlayerAttackCombo : MonoBehaviour
{
    private AnimatorManager animatorManager;

    Vector3 mousePos;
    //Target Lock
    [SerializeField] private float radius;
    [SerializeField] private float lockRange;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool enemyIsLocked;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float rotationSpeed;
    private RaycastHit hit;

    //Combo
    [SerializeField] private float attackMoveSpeed;
    public int comboCount = 0;
    [SerializeField] private float defaultComboDur = 3f;
    [SerializeField] private float comboWindow = 0.7f;
    [SerializeField]private float comboTimer = 0.0f;
    [SerializeField]private float comboDuration;
    private int maxComboCount = 4;
    private bool comboStarted = false;


    void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    void Start()
    {
        comboDuration = defaultComboDur;
    }
    // Update is called once per frame
    void Update()
    {
        AttackCombo();
        ComboDurationStart();
    }

    void LockOnTarget()
    {
        GameObject previousTarget = currentTarget;
        //Get Mouse Position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0f;
        if (plane.Raycast(ray, out distance))
        {
            mousePos = ray.GetPoint(distance);
        }
        //Target Lock Enemy Based on Mouse Position - Range
        enemyIsLocked = Physics.SphereCast(transform.position, radius, mousePos - transform.position, out hit, lockRange, targetLayer);

        if (enemyIsLocked)
        {
            //Set Target
            currentTarget = hit.collider.gameObject;
            //Rotate to Target
            Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {

            currentTarget = previousTarget;
        }
    }
    void AttackCombo()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LockOnTarget();
            comboStarted = true;
            if (comboCount < maxComboCount && comboTimer <= 0.0f)
            {
                comboCount++;
                comboTimer = comboWindow;
                animatorManager.Attack("Attack" + comboCount.ToString());
                //play Attack audio
                //Add movement when attacking
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.velocity = transform.forward * attackMoveSpeed;
            }
            else if (comboCount == maxComboCount)
            {
                comboCount = 0;
                comboDuration = defaultComboDur;
            }
        }

        if (comboTimer > 0.0f)
        {
            comboTimer -= Time.deltaTime;
        }
    }
    void ComboDurationStart()
    {
        if (comboStarted)
        {
            comboDuration -= Time.deltaTime;
            if (comboDuration <= 0 )
            {
                comboCount = 0;
                comboDuration = defaultComboDur;
                comboStarted = false;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0f;
        if (plane.Raycast(ray, out distance))
        {
            mousePos = ray.GetPoint(distance);
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, mousePos - transform.position, out hit, lockRange, targetLayer))
        {
            Gizmos.DrawLine(transform.position, hit.point);
            Gizmos.DrawWireSphere(hit.point, radius);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + (mousePos - transform.position).normalized * lockRange);
        }
    }
}
