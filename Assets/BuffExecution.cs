using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffExecution : SkillExecution
{
    private CharacterStats characterStats;

    private void Awake()
    {
        characterStats = gameObject.GetComponentInParent<CharacterStats>();
    }
    override public void ExecuteSkill()
    {
        Debug.Log("Buff called");
        characterStats.isBuffed = true;
        characterStats.bonusATK = 10;
        characterStats.healing = 15;
        characterStats.bonusCrit = 15;
    }

}
