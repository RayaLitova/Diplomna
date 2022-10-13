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

    private void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("isInCombat", true);
    }

    private void FixedUpdate()
    {
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

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * runSpeed;

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
                moveDirection *= dashSpeed;
                controller.Move(moveDirection);
                targetDashCooldownTimer = Time.fixedTime + dashCoolDown;
                targetDashTimer = Time.fixedTime + animator.GetCurrentAnimatorStateInfo(0).length / 3; // dash animation length 
                return;
            }
            else
            {
                animator.SetBool("isMoving", true);
                moveDirection *= animator.GetBool("isInCombat") ? runSpeed : walkSpeed;
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