using System.Collections;
using UnityEngine;

public class CharacterTakeDamage : MonoBehaviour
{
    private CharacterAnimationController animator;
    private CharacterStats characterStats;

    private void Start()
    {
        animator = GetComponent<CharacterAnimationController>();
        characterStats = GetComponent<CharacterStats>();
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTakeDamage(false);
        if (characterStats.Health <= 0f)
            LoadScene.Load("CityScene");
    }
    public void TakeDamage(Transform enemy)
    {
        UI_skillsManage.FinishSkillExecution();
        animator.SetTakeDamage(true);

        int damage = enemy.GetComponent<CharacterStats>().CalcDamageAgainst(characterStats, enemy.GetComponent<SkillStats>());
        characterStats.Health -= damage;
        animator.SetIsDead(characterStats.Health <= 0f);

        StartCoroutine("WaitForAnimationToFinish");
    }
}
