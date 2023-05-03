using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGlassPowerup : MonoBehaviour
{
    public float powerUpPoints;
    public bool isHolding;

    private AnimatorManager animatorManager;
    private UiManager uiManager;
    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        uiManager = GameObject.FindWithTag("UiManager").GetComponent<UiManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.DisplayerPowerup(powerUpPoints);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isHolding = true;
            if(powerUpPoints >= 0)
            {
                powerUpPoints -= 10 * Time.deltaTime;
            }
            else
            {
                powerUpPoints = 0;
            }
        }
        else
        {
            isHolding = false;
        }

    }
    public void Dodge(bool isDodging)
    {
        if (!isDodging)
            return;

        if (powerUpPoints > 0)
        {
            animatorManager.DodgeAnimation();
        }
        else if (powerUpPoints <= 0)
        {
            isDodging = false;
        }
    }
    public void AddPoints(float point)
    {
        powerUpPoints += point;
    }
}
