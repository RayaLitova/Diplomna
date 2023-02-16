using UnityEngine;

public class BuffExecution : SkillExecution
{
    private CharacterStats characterStats;
    [SerializeField] private int bonusATK = 10;
    [SerializeField] private int bonusDEF = 0;
    [SerializeField] private int bonusCrit = 15;
    [SerializeField] private int bonusAgility = 0;
    [SerializeField] private int healing = 20;
    private void Awake()
    {
        characterStats = gameObject.GetComponentInParent<CharacterStats>();
    }
    override public void ExecuteSkill()
    {
        characterStats.isBuffed = true;
        characterStats.bonusATK += bonusATK;
        characterStats.bonusDEF += bonusDEF;
        characterStats.bonusCrit += bonusCrit;
        characterStats.bonusAgility += bonusAgility;
        characterStats.healing += healing;
    }

}
