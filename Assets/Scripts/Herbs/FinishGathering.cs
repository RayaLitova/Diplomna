using UnityEngine;

public class FinishGathering : StateMachineBehaviour
{
    public static GameObject herb;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isGathering", false);
        Destroy(herb);
    }


}
