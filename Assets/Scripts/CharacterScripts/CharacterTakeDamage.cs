using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        if (characterStats.Health <= 0f)
        {
            LoadDungeon.dungeonLevel--;
            SceneManager.LoadScene("CityScene");
        }
    }
    public void TakeDamage(Transform enemy)
    {
        CharacterMovement.isImmobilized = true;
        animator.SetBool("DamageTaken", true);
        int damage = enemy.GetComponent<CharacterStats>().CalcDamageAgainst(characterStats, enemy.GetComponent<SkillStats>());
        characterStats.Health -= damage;
        animator.SetBool("isDead", characterStats.Health <= 0f);
        StartCoroutine("WaitForAnimationToFinish");
    }
}
