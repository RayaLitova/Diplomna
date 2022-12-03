using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : InteractAction
{
    private Animator animator;
    private bool isDoorOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Action()
    {
        Debug.Log("Action");
        animator.SetBool("openDoor", !isDoorOpened);
        animator.SetBool("closeDoor", isDoorOpened);
        isDoorOpened = !isDoorOpened;
    }
}
