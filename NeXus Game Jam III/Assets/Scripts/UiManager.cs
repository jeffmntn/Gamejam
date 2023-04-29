using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    private Image healthBar;

    private void Awake()
    {
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
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
