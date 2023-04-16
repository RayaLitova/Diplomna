using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : Usable
{
    public int damage;
    public float crit;
    public int missChance;
    public float cooldown;
    public enum SkillTypes { damage, buff }
    public SkillTypes skillType;

    public int bonusATK = 0;
    public int bonusDEF = 0;
    public int bonusCrit = 0;
    public int bonusAgility = 0;
    public int bonusMaxHealth = 0;
    public int bonusRegen = 0;
}
