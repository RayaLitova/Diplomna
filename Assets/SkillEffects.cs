using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffects : MonoBehaviour
{
    public static Dictionary<string, Action> effects;

    private void Start()
    {
        effects = new Dictionary<string, Action>()
        {
            { "AOE", () => AOE() },
        };

    }

    public static void ApplyEffect(string name)
    {
        effects[name]();
    }

    public static void ApplyEffect(string[] name)
    {
        foreach (string flag in name)
            effects[flag]();
        
    }

    private void AOE()
    {
        Debug.Log("AOE");
    }
}
