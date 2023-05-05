using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    private Image healthBar;
    private Image powerupBar;
    private Text proceedTxt;
    public Image interact;
    public GameObject loseObj;
    private void Awake()
    {
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
        powerupBar = GameObject.FindWithTag("PowerupBar").GetComponent<Image>();
        proceedTxt = GameObject.FindWithTag("ProceedText").GetComponent<Text>();
        interact = GameObject.FindWithTag("Interact").GetComponent<Image>();
        loseObj.GetComponent<GameObject>();
    }
    public void YouLose()
    {
        loseObj.SetActive(true);
    }
    public void InteractEnter()
    {
        interact.enabled = true;
    }
    public void InteractExit()
    {
        interact.enabled = false;
    }
    public void DisplayerPowerup(float powerupValue)
    {
        powerupValue /= 100f;
        if (powerupValue < 0)
        {
            powerupValue = 0;
        }
        powerupBar.fillAmount = powerupValue;
    }

    public void DisplayHealth(float healthValue)
    {
        healthValue /= 100f;
        if (healthValue < 0)
        {
            healthValue = 0;
        }
        healthBar.fillAmount = healthValue;
    }

    public void DisableProceed()
    {
        proceedTxt.enabled = false;
    }
    public void EnableProceed()
    {
        proceedTxt.enabled = true;
    }
}
