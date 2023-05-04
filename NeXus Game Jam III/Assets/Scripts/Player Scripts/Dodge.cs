using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public float powerUpPoints;

    private AnimatorManager animatorManager;
    private UiManager uiManager;
    public float dashDistance;
    public bool isDodging;
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
        if (Input.GetKeyDown(KeyCode.Space) && !isDodging)
        {
            IsDodge();
        }

    }
    public void IsDodge()
    {
        if (powerUpPoints > 0)
        {
            isDodging = true;
            powerUpPoints -= 10;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = -transform.forward * dashDistance;
            animatorManager.DodgeAnimation();
        }
        else if (powerUpPoints <= 0)
        {
            isDodging = false;
            powerUpPoints = 0;
        }
    }
    public void AddPoints(float point)
    {
        if (powerUpPoints <= 100)
        {
            powerUpPoints += point;
        }
    }
}
