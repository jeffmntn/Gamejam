using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float defaultHealth = 100;
    [SerializeField]private float currentHealth;
    private AnimatorManager animatorManager;
    private EnemyAI enemyAi;
    private UiManager uiManager;
    private bool isDead;
    public bool isPlayer;
    private Dodge dodge;
    public static float playerHealth = 100;
    public GameObject potion;
    private void Awake()
    {     
        animatorManager = GetComponentInChildren<AnimatorManager>();
        if(isPlayer)
        {
            uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
        }
        else
        {
            dodge = GameObject.FindWithTag("Player").GetComponent<Dodge>();
            enemyAi = GetComponent<EnemyAI>();
            potion.GetComponent<GameObject>();
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
            uiManager.DisplayHealth(playerHealth);
        }     

    }
    public void ApplyDamage(float damage, bool knockDown)
    {
        if (isDead)
            return;

        if(!isPlayer)
        {
            currentHealth -= damage;
            animatorManager.HitSfx();
            if (currentHealth <= 0f)
            {
                isDead = true;
                dodge.AddPoints(20);
                if(Random.Range(0,2) < 1)
                {
                    Instantiate(potion, transform.position + transform.up * 1, Quaternion.identity);
                }
                animatorManager.DeathAnimation();
                Destroy(gameObject, 5f);
                return;
            }

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
        else
        {
            playerHealth -= damage;
            if (playerHealth <= 0f)
            {
                 animatorManager.DeathAnimation();               
            }
        }
    }
}
