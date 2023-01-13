using System.Collections;
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
                    action.Action(); //Interact
                    yield return new WaitForSeconds(3f); //Interaction cooldown
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
            ShowHide_InteractionUI.textString = action.description; //Show available interaction
            interactEnabled = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactEnabled = false;
    }
}
