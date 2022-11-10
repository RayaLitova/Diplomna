using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffects : MonoBehaviour
{
    public static Dictionary<string, Action> effects;

    private static Collider enemyCollider;
    private void Start()
    {
        effects = new Dictionary<string, Action>()
        {
            { "AOE", () =>  AOE() },
        };

    }
    private void AOE()
    {
        Debug.Log("AOE");
    }
    public static void ApplyEffect(string name)
    {
        effects[name]();
    }

    public static void ApplyEffects(string[] name, Collider collider)
    {
        enemyCollider = collider;
        foreach (string flag in name)
            effects[flag]();
        
    }
}
