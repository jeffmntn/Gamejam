using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehavior
{
    Idle,
    Chase,
    Attack,
    Flee
}

public class EnemyAI : MonoBehaviour
{
    private AnimatorManager animatorManager;
    private Transform playerTarget;

    public EnemyBehavior currentBehavior;
    public float moveSpeed = 5f;
    public float fleeSpeed = 5f;

    public float attackDist = 1f;
    private bool attackedOnce;
    [SerializeField] private float defaultNextAtkTime = 2f;
    private float nextAtkTime;
    [SerializeField] private float defaultAttackModeTimer = 2f;
    private float attackModeTimer;
    [SerializeField] private float randomFleeDistance;
    [SerializeField] private float maxFleeDistance;
    [SerializeField] private float fleePlayerAfterAtk = 1f;
    [SerializeField]float distToPlayer;
    public Vector3 backPos;

    void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerTarget = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        SetBehavior(EnemyBehavior.Chase);
        attackModeTimer = defaultAttackModeTimer;
        nextAtkTime = defaultNextAtkTime;
    }
    private void Update()
    {
        EnemyAIBehavior();
    }
    public void SetBehavior(EnemyBehavior newBehavior)
    {
        currentBehavior = newBehavior;
    }

    void EnemyAIBehavior()
    {
        switch (currentBehavior)
        {
            case EnemyBehavior.Idle:
                animatorManager.EnemyMovementAnimation(false);
                attackModeTimer -= Time.deltaTime;
                if (attackModeTimer <= 0)
                {
                    SetBehavior(EnemyBehavior.Chase);
                }
                break;

            case EnemyBehavior.Chase:
                if (playerTarget != null)
                {
                    transform.LookAt(playerTarget);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    animatorManager.EnemyMovementAnimation(true);
                    if (Vector3.Distance(transform.position, playerTarget.position) <= attackDist)
                    {
                        animatorManager.EnemyMovementAnimation(false);
                        SetBehavior(EnemyBehavior.Attack);
                    }
                }
                break;

            case EnemyBehavior.Attack:
                if (playerTarget != null)
                {
                    if (Vector3.Distance(transform.position, playerTarget.position) > attackDist)
                    {
                        SetBehavior(EnemyBehavior.Chase);
                    }
                    nextAtkTime -= Time.deltaTime;
                    if (nextAtkTime < 0 && !attackedOnce)
                    {
                        animatorManager.EnemyAttack(Random.Range(0, 3));
                        attackedOnce = true;
                    }
                    else if (attackedOnce)
                    {
                        fleePlayerAfterAtk -= Time.deltaTime;
                    }
                    if (fleePlayerAfterAtk <= 0)
                    {
                        randomFleeDistance = Random.Range(3, 6);

                        backPos = playerTarget.position;
                        SetBehavior(EnemyBehavior.Flee);
                    }
                }
                break;

            case EnemyBehavior.Flee:
                if (playerTarget != null)
                {
                    transform.LookAt(playerTarget);
                    distToPlayer = Vector3.Distance(transform.position, backPos);
                    //lastplayerposition
                    if (distToPlayer < randomFleeDistance)
                    {
                        transform.Translate(-Vector3.forward * fleeSpeed * Time.deltaTime);
                        animatorManager.EnemyFleeAnimation(true);
                    }
                    else if (distToPlayer >= randomFleeDistance)
                    {
                        animatorManager.EnemyFleeAnimation(false);
                        nextAtkTime = defaultNextAtkTime;
                        fleePlayerAfterAtk = 1f;
                        attackedOnce = false;
                        attackModeTimer = defaultAttackModeTimer;
                        SetBehavior(EnemyBehavior.Idle);
                    }
                }
                break;
            default:
                Debug.LogError("Unknown enemy behavior: " + currentBehavior);
                break;
        }
    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            SetBehavior(EnemyBehavior.Idle);
            animatorManager.EnemyFleeAnimation(false);
            nextAtkTime = defaultNextAtkTime;
            fleePlayerAfterAtk = 1f;
            attackedOnce = false;
            attackModeTimer = defaultAttackModeTimer;
        }
    }
}