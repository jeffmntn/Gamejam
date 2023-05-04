using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    public GameObject leftHand, rightHand, head;

    public float getUpTimer = 2f;

    private PlayerController playerController;
    private PlayerAttackCombo playerAttack;
    private EnemyAI enemyAi;
    private Dodge dodge;
    private AudioSource audioSource;
    [SerializeField] private AudioClip attackSfx, hitSfx;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        dodge = GameObject.FindWithTag("Player").GetComponent<Dodge>();
        animator = GetComponent<Animator>();
        if(gameObject.CompareTag("Enemy"))
        {
            enemyAi = GetComponentInParent<EnemyAI>();
        }
        if (gameObject.CompareTag("Player"))
        {
            playerController = GetComponentInParent<PlayerController>();
            playerAttack = GetComponentInParent<PlayerAttackCombo>();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    //Universal Animation
    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
    public void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }
    public void DisableMovement()
    {
        if (enemyAi)
        {
            enemyAi.enabled = false;
            transform.parent.gameObject.layer = 0;
            transform.parent.gameObject.tag = "Untagged";
            transform.gameObject.tag = "Untagged";
        }
        if (playerController)
        {
            playerController.enabled = false;
        }
    }

    public void EnableMovement()
    {

        if (enemyAi)
        {
            enemyAi.enabled = true;
            transform.parent.gameObject.layer = 6;
            transform.parent.gameObject.tag = "Enemy";
            transform.gameObject.tag = "Enemy";
        }
        if (playerController)
        {
            playerController.enabled = true;
        }
    }

    public void DisableAttack()
    {
        if (playerController)
        {
            playerAttack.enabled = false;
        }
    }
    //Player Animation
    public void MovementAnimation(float Speed)
    {
        animator.SetFloat("Speed",Speed);
    }

    public void CombatToIdle(bool isCombat)
    {
        animator.SetBool("isCombat", isCombat);
    }
    public void Attack(string attackNum)
    {
        AttackSfx();
        animator.SetTrigger(attackNum);
        DisableMovement();
    }

    public void DodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }
    //EnemyAnimation
    public void DisableMoveOnAttack()
    {
        enemyAi.enabled = false;
    }
    public void EnableMoveAfterAttack()
    {
        enemyAi.enabled = true;
    }
    public void EnemyMovementAnimation(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void EnemyFleeAnimation(bool isFleeing)
    {
        animator.SetBool("isFleeing", isFleeing);
    }

    public void EnemyAttack(int AttackNum)
    {
        if(AttackNum == 0)
        {
            animator.SetTrigger("Attack1");
        }
        if (AttackNum == 1)
        {
            animator.SetTrigger("Attack2");
        }
        if (AttackNum == 2)
        {
            animator.SetTrigger("Attack3");
        }
    }

    public void EnemyKnockedDown()
    {
        animator.SetTrigger("KnockedDown");
    }

    void GetUp()
    {
        StartCoroutine(EnemyGetUpAfterTime());
    }
    IEnumerator EnemyGetUpAfterTime()
    {
        yield return new WaitForSeconds(getUpTimer);
        animator.SetTrigger("GetUp");
    }

    //Event Contoller
    //Attack Collider Animation Controller

    void DodgeOn()
    {
        playerController.enabled = false;
        playerAttack.enabled = false;
    }
    void DodgeOff()
    {
        dodge.isDodging = false;
        playerController.enabled = true;
        playerAttack.enabled = true;
        
    }
    void LeftHandColliderOn()
    {
        leftHand.SetActive(true);
    }
    void LeftHandColliderOff()
    {
        if(leftHand.activeInHierarchy)
        {
            leftHand.SetActive(false);
        }
    }
    void RightHandColliderOn()
    {
        rightHand.SetActive(true);
    }

    void RightHandColliderOff()
    {
        if (rightHand.activeInHierarchy)
        {
            rightHand.SetActive(false);
        }
    }

    void HeadColliderOn()
    {
        if (head == null)
            return;
        head.SetActive(true);
    }

    void HeadColliderOff()
    {
        if (head == null)
            return;
        if (head.activeInHierarchy)
        {
            head.SetActive(false);
        }
    }

    //Audio

    public void AttackSfx()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = attackSfx;
        audioSource.Play();
    }
    public void HitSfx()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = hitSfx;
        audioSource.Play();
    }
}
