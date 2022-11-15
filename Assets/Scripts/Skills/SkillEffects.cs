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
            { "Burn", () => Burn() }
        };

    }
    private void Burn()
    {
        //not calling
        Debug.Log("Burn called");
        StartCoroutine("ApplyBurn");
    }

    private IEnumerator ApplyBurn()
    {
        for (int i = 0; i < 3; i++)
        {
            enemyCollider.GetComponent<EnemyTakeDamage>().TakeDOTdamage(3);
            yield return new WaitForSeconds(3f);
        }
    }
    private void AOE()
    {
        Debug.Log("AOE");
    }
    public static void ApplyEffect(string name)
    {
        effects[name]();
    }

    public static void ApplyEffects(string[] effectFlags, Collider collider)
    {
        Debug.Log(effectFlags[0]);
        enemyCollider = collider;
        foreach (string flag in effectFlags)
            effects[flag]();
        
    }
}
