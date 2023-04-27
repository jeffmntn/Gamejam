using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private PlayerAnimator playerAnimator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    private Vector3 moveDir;

    void Update()
    {
        //Movement Input
        Vector2 playerInput = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            playerInput.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerInput.x = +1;
        }

        //Attacking
        if (!playerAnimator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Combo 1") && !playerAnimator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Combo 2") && !playerAnimator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Combo 3"))
        {
            //Sprint
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            //Movement
            playerInput = playerInput.normalized;
            moveDir = new Vector3(playerInput.x, 0, playerInput.y);
            transform.position += moveDir * currentSpeed * Time.deltaTime;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        if (attackPoint is null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public Vector3 MoveDir()
    {
        return moveDir;
    }
}
