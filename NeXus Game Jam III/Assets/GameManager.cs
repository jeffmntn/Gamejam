using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public EnemyManager[] enemyManagers;
    public GameObject firstAreaSpawner;
    bool allAreasCleared;
    void Start()
    {
        foreach (EnemyManager enemyManager in enemyManagers)
        {
            enemyManager.nextAreaCollider.isTrigger = false;
            if (enemyManager.nextAreaSpawner != null)
            {
                enemyManager.nextAreaSpawner.SetActive(false);
            }
        }

        if (firstAreaSpawner != null)
        {
            firstAreaSpawner.SetActive(true);
        }
    }

    void Update()
    {
        allAreasCleared = true;
        foreach (EnemyManager enemyManager in enemyManagers)
        {
            if (!enemyManager.nextAreaCollider.isTrigger)
            {
                allAreasCleared = false;
                break;
            }
        }

        if (allAreasCleared)
        {
            Debug.Log("All areas is cleared");
            //Next scene 
        }
    }
}
