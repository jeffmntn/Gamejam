using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private Animator playerAnimator;


    public bool isAttacking;
    public int combo;

    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Animations
        if (playerController.MoveDir() == Vector3.zero)
        {
            playerAnimator.SetFloat("Speed", 0f);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimator.SetFloat("Speed", 1f);
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0.4f);
        }
        AttackCombo();
    }


    //Enemy in front Detection
    public void Attack()
    {
        Collider[] enemyCol = Physics.OverlapSphere(playerController.attackPoint.position, playerController.attackRange, playerController.enemyLayer);

        foreach (var enemy in enemyCol)
        {
            Debug.Log("Enemy Detected");
        }
    }

    public void AttackCombo()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            playerAnimator.SetTrigger("" + combo);
        }
    }

    public void StartCombo()
    {
        isAttacking = false;
        if(combo < 3)
        {
            combo++;
        }
    }
    public void EndCombo()
    {
        isAttacking = false;
        combo = 0;
    }
}
