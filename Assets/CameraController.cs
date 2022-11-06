using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Quaternion camRotation;
    void Start()
    {
        camRotation = transform.localRotation;
        
    }

    void Update()
    {
        camRotation.x -= Input.GetAxis("Mouse Y");
        camRotation.y += Input.GetAxis("Mouse X");

        camRotation.x = Mathf.Clamp(camRotation.x, -45, 45);
        camRotation.y = Mathf.Clamp(camRotation.y, -45, 45);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
       //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, camRotation, 0.1f);
    }
}
