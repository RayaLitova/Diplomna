using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTakeDamage : MonoBehaviour
{
    private Animator animator;
    private CharacterStats characterStats;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("DamageTaken", false);
    }
    public void TakeDamage(Transform enemy)
    {
        Debug.Log("Damage Taken");
        animator.SetBool("DamageTaken", true);
        characterStats.Health -= enemy.GetComponent<CharacterStats>().CalcDamageAgainst(characterStats, enemy.GetComponent<SkillStats>());
        animator.SetFloat("Health", characterStats.Health / characterStats.MaxHealth);
        StartCoroutine("WaitForAnimationToFinish");
    }
}
