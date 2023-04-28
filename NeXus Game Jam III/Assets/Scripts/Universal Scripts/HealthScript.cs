using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private AnimatorManager animatorManager;
    private EnemyController enemyController;

    private bool isPlayerDead;
    public bool isPlayer;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }
    public void ApplyDamage(float damage, bool knockDown)
    {
        if (isPlayerDead)
            return;

        health -= damage;

        if(health <=0f)
        {
            //Death animation
            isPlayerDead = true;

            if(isPlayer)
            {

            }
            return;
        }

        if(!isPlayer)
        {
            if(knockDown)
            {
                if(Random.Range(0,2) > 0)
                {
                    animatorManager.EnemyKnockedDown();
                }
            }
            else
            {
                if(Random.Range(0,3) > 1)
                {
                    //Hit/Knockback Animation
                }
            }
        }
    }
}
