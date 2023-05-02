using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemiesInArea;
    public Collider areaCollider;
    public GameObject nextAreaSpawner;
    [SerializeField]bool allEnemiesDead;
    void Start()
    {
        areaCollider.isTrigger = false;
    }

    void Update()
    {
        // Check if all enemies are dead
        
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

        // Can Proceed
        if (allEnemiesDead)
        {
            areaCollider.isTrigger = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
            nextAreaSpawner.SetActive(true);       
    }
}
