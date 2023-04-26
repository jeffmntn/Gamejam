using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            playerInput.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerInput.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerInput.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerInput.x = +1;
        }

        //Sprint
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //Movement
        playerInput = playerInput.normalized;
        Vector3 moveDir = new Vector3(playerInput.x,0, playerInput.y);
        transform.position += moveDir * currentSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }
}
