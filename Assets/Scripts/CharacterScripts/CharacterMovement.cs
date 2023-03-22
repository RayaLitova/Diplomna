using UnityEngine;
using Cinemachine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform mainCamera;

    private CharacterAnimationController animationController;
    private CharacterController controller;

    private float dashEndTime = 0.0f;
    private float dashCooldownEndTime = 0.0f;
    private float dashCooldown = 3.0f;

    [SerializeField] private float gravity;

    private float moveX;
    private float moveZ;
    private Vector3 moveDirection;

    public static bool isImmobilised = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animationController = GetComponent<CharacterAnimationController>();
        animationController.SetIsInCombat(true);
        isImmobilised = false;
    }
    private void FixedUpdate()
    {
        GravityHandler();

        if (Cursor.lockState == CursorLockMode.None || isImmobilised) // disable movement on alt press or application unfocused
        {
            Immobilise();
            return;
        }
        
        mainCamera.GetComponent<CinemachineBrain>().enabled = true;

        if (Time.fixedTime < dashEndTime) // disable movement while dashing
        {
            controller.Move(new Vector3(moveX, 0, moveZ) * dashSpeed);
            return;
        }
        animationController.SetDash(false);
        MovementDirectionHandler();

        if (moveDirection == Vector3.zero)
        {
            animationController.SetMove(false);
            return;
        }
        
        RotationHandler();
        MoveHandler();//handles run/walk and dash
    }

    private void Immobilise()
    {
        animationController.SetDash(false);
        animationController.SetMove(false);
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;
    }

    private void GravityHandler()
    {
        controller.Move(new Vector3(0, gravity, 0));
    }

    private void MovementDirectionHandler()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        if (moveDirection == Vector3.zero)
            return;

        moveDirection = Quaternion.AngleAxis(mainCamera.rotation.eulerAngles.y, Vector3.up) * moveDirection;//rotate towards camera
        moveDirection.Normalize();
    }

    private void RotationHandler()
    {
        Vector3 rotationDirection = new Vector3(-moveZ, 0, moveX); // rotate towards movement direction
        rotationDirection = Quaternion.AngleAxis(mainCamera.rotation.eulerAngles.y, Vector3.up) * rotationDirection; // rotate to match camera
        rotationDirection.Normalize();
        Quaternion toRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
    }

    private void MoveHandler()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Time.fixedTime > dashCooldownEndTime) // dash
        {
            animationController.SetDash(true);
            moveDirection *= dashSpeed;
            controller.Move(moveDirection);
            dashCooldownEndTime = Time.fixedTime + dashCooldown;
            dashEndTime = Time.fixedTime + animationController.GetCurrentAnimatorStateInfo().length / 3; // dash animation length 
        }
        else // run
        {
            animationController.SetMove(true);
            moveDirection *= animationController.GetIsInCombat() ? runSpeed : walkSpeed;
            controller.Move(moveDirection);
        }
    }

}