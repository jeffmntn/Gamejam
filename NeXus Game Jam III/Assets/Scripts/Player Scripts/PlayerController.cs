using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float rotationSpeed;

    private Camera mainCamera;
    private AnimatorManager animationManager;
    private Vector3 moveDir;
    Vector3 playerForward;

    void Awake()
    {
        animationManager = GetComponentInChildren<AnimatorManager>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        //Movement Input
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

        //Movement Animation Controller
        if (playerForward == Vector3.zero)
        {
            animationManager.MovementAnimation(0);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animationManager.MovementAnimation(1f);
        }
        else
        {
            animationManager.MovementAnimation(0.4f);
        }

        //Sprint
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //Movement
        playerInput = playerInput.normalized;

        Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = (playerInput.y * cameraForward + playerInput.x * mainCamera.transform.right).normalized;
        playerForward = moveDir * currentSpeed;
        transform.position += playerForward * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }
}
