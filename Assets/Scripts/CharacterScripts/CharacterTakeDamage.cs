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
        CharacterMovement.isImmobilized = false;
        animator.SetBool("DamageTaken", false);
    }
    public void TakeDamage(Transform enemy)
    {
        animator.SetBool("DamageTaken", true);
        CharacterMovement.isImmobilized = true;
        characterStats.Health -= enemy.GetComponent<CharacterStats>().CalcDamageAgainst(characterStats, enemy.GetComponent<SkillStats>());
        Debug.Log(characterStats.Health);
        animator.SetBool("isDead", characterStats.Health <= 0f);
        StartCoroutine("WaitForAnimationToFinish");
    }
}
