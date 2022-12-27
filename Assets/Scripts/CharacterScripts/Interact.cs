using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool interactEnabled = false;
    private InteractAction action;

    private void Start()
    {
        StartCoroutine("CheckForInteraction");
    }

    private IEnumerator CheckForInteraction()
    {
        while (true)
        {
            if (interactEnabled)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    action.Action();
                    yield return new WaitForSeconds(3f);
                }
            }
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            action = other.gameObject.GetComponent<InteractAction>();
            ShowHide_InteractionUI.text = action.description;
            interactEnabled = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactEnabled = false;
    }
}
