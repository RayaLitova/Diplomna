using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private CharacterController controller;
    [SerializeField] private CharacterController cameraController;
    [SerializeField] private Transform camera;

    private Vector3 moveDirection;

    private void Start()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * runSpeed;

        cameraController.Move(moveDirection * magnitude);
        controller.Move(moveDirection * magnitude);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        }
    }

}