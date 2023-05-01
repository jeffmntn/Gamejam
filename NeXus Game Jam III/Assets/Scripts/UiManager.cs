using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    private Image healthBar;
    private Image powerupBar;
    private void Awake()
    {
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
        powerupBar = GameObject.FindWithTag("PowerupBar").GetComponent<Image>();
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
}
