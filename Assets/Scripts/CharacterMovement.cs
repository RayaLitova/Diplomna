using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int runSpeed;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int dashSpeed;

    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator animator;

    private float targetDashTimer = 0.0f;
    private float dashCoolDown = 3.0f;

    private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("isInCombat", true);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && moveDirection.y < 0)
            moveDirection.y = -2.0f; //to avoid ground issues

        animator.SetBool("Dash", false);

        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        Debug.Log("Z: " + moveZ);
        Debug.Log("X: " + moveX);

        moveDirection = new Vector3(moveX, 0, moveZ);

        if (moveDirection != Vector3.zero && !isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Time.fixedTime > targetDashTimer)
            {
                animator.SetBool("Dash", true);
                moveDirection *= dashSpeed;
                controller.Move(moveDirection);
                targetDashTimer = Time.fixedTime + dashCoolDown;
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