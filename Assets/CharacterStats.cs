using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public float HealthRegen;

    public float Mana;
    public float MaxMana;
    public float ManaRegen;

    public int ATK;
    public int DEF;
    public int Crit;
    public int Agility;
    public int MissChance;

    private float lastTime = 0.0f;

    public int CalcDamageAgainst(CharacterStats enemy)
    {
        if (Random.Range(0, 100) <= MissChance + enemy.Agility)
            return 0;

        int damage = ATK + (Random.Range(0, 100) <= Crit ? ATK : 0);
        damage -= (int)((damage / 100.0f) * enemy.DEF);
        
        return damage;
    }

    private void Update()
    {
        if (Time.time < lastTime + 1)
            return;

        lastTime = Time.time;
        Mana = Mathf.Min(MaxMana, Mana + ManaRegen);
        Health = Mathf.Min(MaxHealth, Health + HealthRegen);
    }
}
