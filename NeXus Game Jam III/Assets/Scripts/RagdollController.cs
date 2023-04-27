using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public BoxCollider bodyCol;
    public GameObject rig;
    public Animator animator;
    public bool ragdollMode = false;

    Collider[] ragdollCol;
    Rigidbody[] ragdollRb;
    void Start()
    {
        GetRagdoll();
        RagdollOff();
    }
    void GetRagdoll()
    {
        ragdollCol = rig.GetComponentsInChildren<Collider>();
        ragdollRb = rig.GetComponentsInChildren<Rigidbody>();
    }
    public void RagdollOn()
    {
        foreach (Collider col in ragdollCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rb in ragdollRb)
        {
            rb.isKinematic = false;
        }

        animator.enabled = false;
        bodyCol.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void RagdollOff()
    {
        foreach(Collider col in ragdollCol)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rb in ragdollRb)
        {
            rb.isKinematic = true;
        }

        animator.enabled = true;
        bodyCol.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
