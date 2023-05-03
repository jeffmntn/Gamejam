using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemiesInArea;
    public Collider areaCollider;
    public GameObject nextAreaSpawner;
    [SerializeField]bool allEnemiesDead;

    private UiManager uiManager;
    public bool proceed;
    private void Awake()
    {
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }
    void Start()
    {
        areaCollider.isTrigger = false;
        proceed = false;
        uiManager.DisableProceed();
    }

    void Update()
    {
        // Check if all enemies are dead
        if(!proceed)
        {
            foreach (GameObject enemy in enemiesInArea)
            {
                if (enemy != null)
                {
                    allEnemiesDead = false;
                    break;
                }
                else
                {
                    allEnemiesDead = true;
                }
            }
        }       
        // Can Proceed
        if (allEnemiesDead)
        {
            proceed = true;
            areaCollider.isTrigger = true;
            uiManager.EnableProceed();
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnEnemies();
            uiManager.DisableProceed();
            allEnemiesDead = false;
        }
    }

    void SpawnEnemies()
    {
            nextAreaSpawner.SetActive(true);
    }
}
