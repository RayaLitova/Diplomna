using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    public int HealthRegen;

    public int Mana;
    public int MaxMana;
    public int ManaRegen;

    public int ATK;
    public int DEF;
    public int Crit;
    public int Agility;
    public int MissChance;

    public int CalcDamageAgainst(CharacterStats enemy)
    {
        if (Random.Range(0, 100) <= MissChance + enemy.Agility)
            return 0;

        int damage = ATK + (Random.Range(0, 100) <= Crit ? ATK : 0);
        damage -= (int)((damage / 100.0f) * enemy.DEF);
        
        return damage;
    }
}
