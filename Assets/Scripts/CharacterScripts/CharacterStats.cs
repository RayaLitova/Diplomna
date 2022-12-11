using System.Collections;
using System.Collections.Generic;
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

    [System.NonSerialized] public int bonusATK;
    [System.NonSerialized] public int bonusDEF;
    [System.NonSerialized] public int bonusCrit;
    [System.NonSerialized] public int bonusAgility;
    [System.NonSerialized] public int healing;

    [System.NonSerialized] public bool isBuffed = false;


    [SerializeField] private Image HealthBar = null;

    private float lastTime = 0.0f;

    

    public int CalcDamageAgainst(CharacterStats enemy, SkillStats skill)
    {
        if (Random.Range(0, 100) <= MissChance + skill.missChance + enemy.getAgility())
            return 0;

        int damage = getATK() + skill.damage + (Random.Range(0, 100) <= getCrit() + skill.crit ? getATK() + skill.damage : 0);
        damage -= (int)((damage / 100.0f) * enemy.getDEF());
        
        return damage;
    }

    private void Update()
    {
        if(HealthBar != null) //remove this after adding health bars to enemies
            HealthBar.fillAmount = Health / MaxHealth;
        if (Time.time < lastTime + 1)
            return;

        lastTime = Time.time;
        Health = Mathf.Min(MaxHealth, Health + HealthRegen);
    }

    public void ResetBuffs()
    {
        isBuffed = false;
        bonusCrit = 0;
        bonusAgility = 0;
        bonusATK = 0;
        bonusDEF = 0;
        healing = 0;
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

}
