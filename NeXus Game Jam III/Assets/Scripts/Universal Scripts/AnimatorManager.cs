using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    public GameObject leftHand, rightHand, head;

    public float getUpTimer = 2f;

    private PlayerController playerController;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        if(gameObject.CompareTag("Enemy"))
        {
            enemyController = GetComponentInParent<EnemyController>();
        }
        if (gameObject.CompareTag("Player"))
        {
            playerController = GetComponentInParent<PlayerController>();
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
    //Player Animation
    public void MovementAnimation(float Speed)
    {
        animator.SetFloat("Speed",Speed);
    }

    public void CombatToIdle(bool isCombat)
    {
        animator.SetBool("isCombat", isCombat);
    }
    public void Attack1()
    {
        animator.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        animator.SetTrigger("Attack2");
    }

    public void Attack3()
    {
        animator.SetTrigger("Attack3");
    }

    //EnemyAnimation
    public void EnemyMovementAnimation(bool isMoving)
    {
        animator.SetBool("Movement", isMoving);
    }

    public void DisableMovement()
    {
        if(enemyController)
        {
            enemyController.enabled = false;
            transform.parent.gameObject.layer = 0;
            transform.parent.gameObject.tag = "Untagged";
            transform.gameObject.tag = "Untagged";
        }
        if(playerController)
        {
            playerController.enabled = false;
        }
    }

    public void EnableMovement()
    {

        if(enemyController)
        {
            enemyController.enabled = true;
            transform.parent.gameObject.layer = 6;
            transform.parent.gameObject.tag = "Enemy";
            transform.gameObject.tag = "Enemy";
        }
        if (playerController)
        {
            playerController.enabled = true;
        }
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

    //Attack Collider Animation Controller


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
        head.SetActive(true);
    }

    void HeadColliderOff()
    {
        if (head.activeInHierarchy)
        {
            head.SetActive(false);
        }
    }


}
