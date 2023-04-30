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
    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float fleePlayerAfterAtk = 1f;


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
        Debug.Log(maxDistanceToPlayer);
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
                if(attackModeTimer <= 0)
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
                    if(Vector3.Distance(transform.position, playerTarget.position) > attackDist)
                    {
                        SetBehavior(EnemyBehavior.Chase);
                    }
                    nextAtkTime -= Time.deltaTime;
                    if (nextAtkTime < 0 && !attackedOnce)
                    {
                        animatorManager.EnemyAttack(Random.Range(0, 3));
                        attackedOnce = true;              
                    }
                    else if(attackedOnce)
                    {
                        fleePlayerAfterAtk -= Time.deltaTime;
                    }
                    if(fleePlayerAfterAtk <=0)
                    {
                        maxDistanceToPlayer = Random.Range(3, 6);
                        SetBehavior(EnemyBehavior.Flee);
                    }
                }
                break;

            case EnemyBehavior.Flee:
                if (playerTarget != null)
                {
                    transform.LookAt(playerTarget);
                    float distToPlayer = Vector3.Distance(transform.position, playerTarget.position);
                    if (distToPlayer < maxDistanceToPlayer)
                    {
                        transform.Translate(-Vector3.forward * fleeSpeed * Time.deltaTime);
                        animatorManager.EnemyFleeAnimation(true);
                    }
                    else if (distToPlayer >= maxDistanceToPlayer)
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
}