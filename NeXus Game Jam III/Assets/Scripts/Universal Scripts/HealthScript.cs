using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private AnimatorManager animatorManager;
    private EnemyAI enemyAi;
    private UiManager uiManager;
    private bool isDead;
    public bool isPlayer;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        if(isPlayer)
        {
            uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
        }
    }
    private void Update()
    {
        if(isPlayer)
        uiManager.DisplayHealth(health);
    }
    public void ApplyDamage(float damage, bool knockDown)
    {
        if (isDead)
            return;

        health -= damage;
        if(health <=0f)
        {
            animatorManager.DeathAnimation();
            Destroy(gameObject, 5f);
            isDead = true;
            if(isPlayer)
            {

            }
            return;
        }

        if(!isPlayer)
        {
            if(knockDown)
            {               
                animatorManager.EnemyKnockedDown();
            }
            else
            {
                    animatorManager.HitAnimation();                
            }
        }
    }
}
