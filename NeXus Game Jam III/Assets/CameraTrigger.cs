using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraTrigger : MonoBehaviour
{
    CinemachineVirtualCamera sideScrollCam;
    CinemachineVirtualCamera thirdPersonCam;
    void Awake()
    {
        sideScrollCam = GameObject.FindGameObjectWithTag("Side Scrolling Camera").GetComponent<CinemachineVirtualCamera>();
        thirdPersonCam = GameObject.FindGameObjectWithTag("3rd Person Camera").GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sideScrollCam.Priority = 0;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sideScrollCam.Priority = 2;
        }
    }
}
