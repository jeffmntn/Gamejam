using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    private Transform target;
    [SerializeField]private float rotationSpeed = 5f;
    private bool targetEnemy;

    private AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            targetEnemy = true;
            animatorManager.CombatToIdle(true);
        }
        else
        {
            targetEnemy = false;
            animatorManager.CombatToIdle(false);
        }
        if(targetEnemy)
        {
            TargetEnemy();
        }
    }

    public void TargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        {
            target = closestEnemy;
        }
        else
        {
            target = null;
        }

        if (target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
