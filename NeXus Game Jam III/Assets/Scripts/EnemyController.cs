using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    private PlayerAnimator enemyAnimator;
    private Rigidbody enemyRb;
    private Transform playerTarget;

    [SerializeField] private float moveSpeed = 5f;
    public float attackDist = 1f;
    private float chasePlayerAfterAtk = 1f;

    [SerializeField]private float currentAtkTime;
    [SerializeField]private float defaultAtkTime = 2f;

    private bool followingPlayer, attackingPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAnimator = GetComponentInChildren<PlayerAnimator>();
        enemyRb = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        followingPlayer = true;
        currentAtkTime = defaultAtkTime;
    }
    // Update is called once per frame
    void Update() 
    {
        FollowTarget();
        AttackTarget();
    }

    void FollowTarget()
    {
        if(!followingPlayer)
        {
            return;
        }
        if(Vector3.Distance(transform.position,playerTarget.position) > attackDist)
        {
            transform.LookAt(playerTarget);
            enemyRb.velocity = transform.forward * moveSpeed;

            if(enemyRb.velocity.sqrMagnitude != 0)
            {
                enemyAnimator.EnemyMovementAnimation(true);
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attackDist)
        {
            enemyRb.velocity = Vector3.zero;
            enemyAnimator.EnemyMovementAnimation(false);

            followingPlayer = false;
            attackingPlayer = true;
        }
    }

    void AttackTarget()
    {
        if(!attackingPlayer)
        {
            return;
        }
        currentAtkTime += Time.deltaTime;
        if(currentAtkTime > defaultAtkTime)
        {
            enemyAnimator.EnemyAttack(Random.Range(0, 3));

            currentAtkTime = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) > attackDist + chasePlayerAfterAtk)
        {
            attackingPlayer = false;
            followingPlayer = true;
        }
    }
}
