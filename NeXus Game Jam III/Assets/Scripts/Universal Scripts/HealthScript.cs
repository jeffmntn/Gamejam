using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float defaultHealth = 100;
    private float currentHealth;
    private AnimatorManager animatorManager;
    private EnemyAI enemyAi;
    private UiManager uiManager;
    private bool isDead;
    public bool isPlayer;
    private EyeGlassPowerup powerup;
    private void Awake()
    {     
        animatorManager = GetComponentInChildren<AnimatorManager>();
        if(isPlayer)
        {
            uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
        }
        else
        {
            powerup = GameObject.FindWithTag("Player").GetComponent<EyeGlassPowerup>();
            enemyAi = GetComponent<EnemyAI>();
        }
    }
    private void Start()
    {  
        if(!isPlayer)
        {
            currentHealth = defaultHealth;
        }
    }
    private void Update()
    {
        if(isPlayer)
        {
            uiManager.DisplayHealth(GameManager.currentHealth);
            currentHealth = GameManager.currentHealth;
            Debug.Log("Health current Scene:" + currentHealth);
        }     

    }
    public void ApplyDamage(float damage, bool knockDown)
    {
        if (isDead)
            return;
        GameManager.currentHealth -= damage;
        if(currentHealth <=0f)
        {
            isDead = true;
            if (isPlayer)
            {
                animatorManager.DeathAnimation();
            }
            else
            {
                powerup.AddPoints(20);
                animatorManager.DeathAnimation();
                Destroy(gameObject, 5f);              
            }
            return;
        }

        if(!isPlayer)
        {
            enemyAi.SetBehavior(EnemyBehavior.Idle);
            if (knockDown)
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
