using UnityEngine;
using System;

public class a : MonoBehaviour
{
    [SerializeField] private int runSpeed;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int dashSpeed;
    [SerializeField] private int rotationSpeed;

    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    private CharacterController controller;
    private Animator animator;

    private float targetDashTimer = 0.0f;
    private float targetDashCooldownTimer = 0.0f;
    private float dashCoolDown = 3.0f;

    private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private Transform camera;

    private float moveX = 0;
    private float moveZ = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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

        moveZ = Input.GetAxisRaw("Vertical");
        moveX = Input.GetAxisRaw("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        rotationDirection = new Vector3(moveZ, 0, -moveX);
        if (moveDirection != Vector3.zero)
        {
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