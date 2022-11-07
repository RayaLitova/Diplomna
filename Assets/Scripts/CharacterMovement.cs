using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform camera;

    private Vector3 moveDirection;

    private Animator animator;

    private float targetDashTimer = 0.0f;
    private float targetDashCooldownTimer = 0.0f;
    private float dashCoolDown = 3.0f;

    private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    private float moveX;
    private float moveZ;
    private bool isInCombat = true;

    private Vector3 velocity;

    private void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("isInCombat", isInCombat);
    }

    private void FixedUpdate()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        if (Time.fixedTime < targetDashTimer) // dash
        {
            controller.Move(new Vector3(moveX, 0, moveZ) * dashSpeed);
            return;

        }

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && moveDirection.y < 0)
            moveDirection.y = -2.0f; //to avoid ground issues

        animator.SetBool("Dash", false);

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        int isDiagonal = (moveX != 0 && moveZ != 0) ? 2 : 1;

        /*moveDirection = moveZ * camera.transform.forward * 5;
        moveDirection += moveX * camera.transform.right * 5;
        if (moveDirection.magnitude > 0)
            velocity = moveDirection;
        else
            velocity = Vector3.zero;
        controller.Move(velocity);*/
        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        float magnitude = Mathf.Clamp01(moveDirection.magnitude);

        controller.Move(moveDirection * magnitude);

        if (moveDirection != Vector3.zero)
        {
            Vector3 rotationDirection = new Vector3(-moveZ, 0, moveX);
            rotationDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * rotationDirection;
            rotationDirection.Normalize();
            Quaternion toRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        }
        if (moveDirection != Vector3.zero && !isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Time.fixedTime > targetDashCooldownTimer)
            {
                animator.SetBool("Dash", true);
                moveDirection *= dashSpeed / isDiagonal;
                controller.Move(moveDirection);
                targetDashCooldownTimer = Time.fixedTime + dashCoolDown;
                targetDashTimer = Time.fixedTime + animator.GetCurrentAnimatorStateInfo(0).length / 3; // dash animation length 
                return;
            }
            else
            {
                animator.SetBool("isMoving", true);
                moveDirection *= animator.GetBool("isInCombat") ? runSpeed / isDiagonal : walkSpeed / isDiagonal;
                animator.SetFloat("MoveSpeed", animator.GetBool("isInCombat") ? 1 : 0);
                controller.Move(moveDirection);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        moveDirection = Vector3.zero;
        moveDirection.y += gravity;
        controller.Move(moveDirection);
    }

}