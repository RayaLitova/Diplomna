using UnityEngine;

public class ImmobilisePlayer : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterMovement.isImmobilised = true; // immobilized while executing skill
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterMovement.isImmobilised = false; // immobilized while executing skill
    }
}
