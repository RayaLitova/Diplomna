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

    static void ApplyEffect(string name)
    {
        effects[name]();
    }

    static void ApplyEffect(string[] name)
    {
        //effects[name]();
    }

    private void AOE()
    {
        Debug.Log("AOE damage");
    }
}
