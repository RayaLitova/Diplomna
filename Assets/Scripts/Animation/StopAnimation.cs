using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UI_skillsManage.FinishSkillExecution();
    }
}
