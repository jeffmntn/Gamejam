using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public EnemyManager[] enemyManagers;
    public GameObject firstAreaSpawner;
    public bool allAreasCleared;

    private UiManager uiManager;
    private void Awake()
    {
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }
    void Start()
    {
        foreach (EnemyManager enemyManager in enemyManagers)
        {
            enemyManager.areaCollider.isTrigger = false;
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
            if (!enemyManager.areaCollider.isTrigger)
            {
                allAreasCleared = false;
                break;
            }
        }
    }

    public void NextLevel()
    {
        if (allAreasCleared)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
            
        }
    }
}
