using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGlassPowerup : MonoBehaviour
{
    public float powerUpPoints;
    public bool isHolding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isHolding = true;
            powerUpPoints -= 300 * Time.deltaTime;
        }
        else
        {
            isHolding = false;
            Time.timeScale = 1f;

        }

        // Check if the hold time has been reached
        if (powerUpPoints > 0 && isHolding)
        {
            // Activate the powerup
            Time.timeScale = 0.05f;
        }
        else if(powerUpPoints <= 0)
        {
            powerUpPoints = 0f;
            Time.timeScale = 1f;
        }
    }
}
