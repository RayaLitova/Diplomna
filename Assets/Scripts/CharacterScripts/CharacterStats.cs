using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public float HealthRegen;

    public int ATK;
    public int DEF;
    public int Crit;
    public int Agility;
    public int MissChance;

    [SerializeField] private GameObject buffedParticles;

    [System.NonSerialized] public int bonusATK;
    [System.NonSerialized] public int bonusDEF;
    [System.NonSerialized] public int bonusCrit;
    [System.NonSerialized] public int bonusAgility;
    [System.NonSerialized] public int healing;
    [System.NonSerialized] public int bonusMaxHealth;
    [System.NonSerialized] public int bonusRegen;

    [System.NonSerialized] public bool isBuffed = false;
    [System.NonSerialized] public int buffsCount = 0;


    [SerializeField] private Image HealthBar = null;

    [System.NonSerialized] public string DamagePopupType; //Crit, Miss, Normal


    public int CalcDamageAgainst(CharacterStats enemy, SkillStats characterSkill)
    {
        DamagePopupType = "Normal";
        if (Random.Range(0, 100) <= MissChance + characterSkill.missChance + enemy.getAgility())
        {
            DamagePopupType = "Miss";
            return 0;
        }

        int damage = getATK() + characterSkill.damage;

        if (Random.Range(0, 100) <= getCrit() + characterSkill.crit)
        {
            damage *= 2;
            DamagePopupType = "Crit";
        }

        damage -= (int)((damage / 100.0f) * enemy.getDEF());
        
        return damage;
    }
    private void Start()
    {
        StartCoroutine("Regen");
    }

    private IEnumerator Regen()
    {
        while (true)
        {
            Health = Mathf.Min(MaxHealth, Health + HealthRegen + bonusRegen);
            yield return new WaitForSeconds(1f);
        }

    }
    private void Update()
    {
        if (isBuffed && !buffedParticles.activeSelf)
            buffedParticles.SetActive(true);
            
        if(HealthBar != null) //remove this after adding health bars to enemies
            HealthBar.fillAmount = Health / MaxHealth;
    }

    public void ResetBuffs()
    {
        ReduceBuffs();
        bonusCrit = 0;
        bonusAgility = 0;
        bonusATK = 0;
        bonusDEF = 0;
        bonusMaxHealth = 0;
        bonusRegen = 0;
        healing = 0;
    }

    public void ReduceBuffs()
    {
        buffsCount--;
        if (buffsCount <= 0)
        {
            isBuffed = false;
            buffedParticles.SetActive(false);
        }
    }

    public void AddBuff()
    {
        buffsCount++;
        isBuffed = true;
    }

    public int getATK()
    {
        return ATK + bonusATK;
    }

    public int getAgility()
    {
        return Agility + bonusAgility;
    }

    public int getCrit()
    {
        return Crit + bonusCrit;
    }

    public int getDEF()
    {
        return DEF + bonusDEF;
    }

    public void AddItem(Item item)
    {
        ATK += item.bonusATK;
        DEF += item.bonusDEF;
        Agility += item.bonusAgility;
        Crit += item.bonusCrit;
        MaxHealth += item.bonusMaxHealth;
        HealthRegen += item.bonusRegen;
        UI_skillsManage.ReduceCooldowns(item.cooldownReduction);
    }

    public void RemoveItem(Item item)
    {
        ATK -= item.bonusATK;
        DEF -= item.bonusDEF;
        Agility -= item.bonusAgility;
        Crit -= item.bonusCrit;
        MaxHealth -= item.bonusMaxHealth;
        HealthRegen -= item.bonusRegen;
        UI_skillsManage.ReduceCooldowns(-item.cooldownReduction);
    }
}
