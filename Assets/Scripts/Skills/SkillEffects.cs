using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffects : MonoBehaviour
{
    public static string[] effects;

    private static Collider enemyCollider;
    private IEnumerator Burn()
    {
        Debug.Log("Burn called");
        for (int i = 0; i < 3; i++)
        {
            enemyCollider.GetComponent<EnemyTakeDamage>().TakeDOTdamage(3);
            yield return new WaitForSeconds(3f);
        }
    }
    private IEnumerator AOE()
    {
        Debug.Log("AOE CALL");
        yield return null;
    }

    private void Start()
    {
        effects = new string[]{
            "AOE",
            "Burn"
        };

    }
    public void ApplyEffect(string name)
    {
        StartCoroutine(name);
    }

    public void ApplyEffects(string[] effectFlags, Collider collider)
    {
        Debug.Log(effectFlags[0]);
        enemyCollider = collider;
        foreach (string flag in effectFlags)
        {
            Debug.Log(flag);
            StartCoroutine(flag);
        }
    }
}
