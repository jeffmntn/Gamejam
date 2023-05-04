using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractShades : MonoBehaviour
{
    private GameManager gameManager;
    UiManager uiManager;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameManager.NextLevel();
            }
            if(gameManager.allAreasCleared)
            {
                uiManager.InteractEnter();
            }
            else
            {
                //Kill all enemies first text
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        uiManager.InteractExit();
    }
}
