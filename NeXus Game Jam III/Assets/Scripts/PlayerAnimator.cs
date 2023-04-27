using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator playerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
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
}
