using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySkill : MonoBehaviour
{
    public void SkillFinishedAnimation()
    {
        Skills_UI.FinishSkillExecution(false);
    }
}
