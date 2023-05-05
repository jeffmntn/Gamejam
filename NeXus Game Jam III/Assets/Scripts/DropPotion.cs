using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(HealthScript.playerHealth <=100)
            {
                HealthScript.playerHealth += 20;
            }
            Destroy(gameObject);
        }
    }
}
