using UnityEngine;

public class BuffExecution : SkillExecution
{
    private CharacterStats characterStats;
    private void Awake()
    {
        characterStats = gameObject.GetComponentInParent<CharacterStats>();
    }
    override public void ExecuteSkill(UI_Buff_additional buff = null)
    {
        characterStats.isBuffed = true;
        characterStats.bonusATK += 10;
        characterStats.healing += 20;
        characterStats.bonusCrit += 15;
    }

}
