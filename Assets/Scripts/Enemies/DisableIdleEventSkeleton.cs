using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIdleEventSkeleton : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isInIdleEvent", false);
        animator.SetFloat("IdleEventNumber", Random.Range(0, 2));
    }
}
