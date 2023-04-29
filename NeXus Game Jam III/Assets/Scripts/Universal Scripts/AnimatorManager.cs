using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator playerAnimator;
    public GameObject leftHand, rightHand, head;

    public float getUpTimer = 2f;

    private PlayerController playerController;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
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

    //Player Animation
    public void MovementAnimation(float Speed)
    {
        playerAnimator.SetFloat("Speed",Speed);
    }

    public void CombatToIdle(bool isCombat)
    {
        playerAnimator.SetBool("isCombat", isCombat);
    }
    public void Attack1()
    {
        playerAnimator.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        playerAnimator.SetTrigger("Attack2");
    }

    public void Attack3()
    {
        playerAnimator.SetTrigger("Attack3");
    }

    //EnemyAnimation
    public void EnemyMovementAnimation(bool isMoving)
    {
        playerAnimator.SetBool("Movement", isMoving);
    }

    public void DisableMovement()
    {
        if(enemyController)
        {
            enemyController.enabled = false;
            transform.parent.gameObject.layer = 0;
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
            playerAnimator.SetTrigger("Attack1");
        }
        if (AttackNum == 1)
        {
            playerAnimator.SetTrigger("Attack2");
        }
        if (AttackNum == 2)
        {
            playerAnimator.SetTrigger("Attack3");
        }
    }

    public void EnemyKnockedDown()
    {
        playerAnimator.SetTrigger("KnockedDown");
    }

    void GetUp()
    {
        StartCoroutine(EnemyGetUpAfterTime());
    }
    IEnumerator EnemyGetUpAfterTime()
    {
        yield return new WaitForSeconds(getUpTimer);
        playerAnimator.SetTrigger("GetUp");
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
