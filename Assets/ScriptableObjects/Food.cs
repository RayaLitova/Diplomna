using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : Executable
{
    public enum FoodType 
    { 
        Tea,
        Potion,
        Food,
        Length
    }
    public FoodType type;

    public int bonusATK = 0;
    public int bonusDEF = 0;
    public int bonusCrit = 0;
    public int bonusAgility = 0;
    public int bonusMaxHealth = 0;
    public int bonusRegen = 0;
    public float cooldownReduction = 0f;
    public int healing = 0;
    public override void Execute()
    {
        if (Time.time < cooldownTimer)
            return;

        ApplyCooldown();
        CharacterStats characterStats = GameObject.Find("Player").GetComponentInParent<CharacterStats>();
        characterStats.AddBuff();
        characterStats.ATK += bonusATK;
        characterStats.DEF += bonusDEF;
        characterStats.Crit += bonusCrit;
        characterStats.Agility += bonusAgility;
        characterStats.MaxHealth += bonusMaxHealth;
        characterStats.HealthRegen += bonusRegen;
        characterStats.Health += healing;

        if (cooldownReduction != 0)
        {
            foreach (var v in FindObjectsOfType<Executable>())
            {
                v.ReduceCooldown(cooldownReduction);
            }
        }
        FindObjectOfType<MonoBehaviour>().StartCoroutine(timer());
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(cooldown);
        ReverseEffect();
    }

    public void ReverseEffect()
    {
        CharacterStats characterStats = GameObject.Find("Player").GetComponentInParent<CharacterStats>();
        characterStats.ATK -= bonusATK;
        characterStats.DEF -= bonusDEF;
        characterStats.Crit -= bonusCrit;
        characterStats.Agility -= bonusAgility;
        characterStats.MaxHealth -= bonusMaxHealth;
        characterStats.HealthRegen -= bonusRegen;
        characterStats.ReduceBuffs();

        if (cooldownReduction != 0)
        {
            foreach (var v in FindObjectsOfType<Executable>())
            {
                v.ReduceCooldown(-cooldownReduction);
            }
        }
    }
}
