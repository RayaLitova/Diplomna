using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool interactEnabled = false;
    private InteractAction action;

    private void Update()
    {
        if (interactEnabled)
        {
            if (Input.GetKeyDown(KeyCode.F))
                action.Action();
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            action = other.gameObject.GetComponent<InteractAction>();
            interactEnabled = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactEnabled = false;
    }
}
