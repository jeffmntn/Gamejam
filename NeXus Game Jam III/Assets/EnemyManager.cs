using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemiesInArea;
    public Collider nextAreaCollider;
    public GameObject nextAreaSpawner;
    bool allEnemiesDead;
    void Start()
    {
        nextAreaCollider.isTrigger = false;
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
            nextAreaCollider.isTrigger = true;
            if (nextAreaSpawner != null)
            {
                nextAreaSpawner.SetActive(true);
            }
        }
    }
}
