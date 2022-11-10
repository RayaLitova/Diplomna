using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Animator animator;
    private float animationTimer;
    private CharacterStats stats;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.fixedTime < animationTimer)
            return;
        if (stats.Health <= 0)
            Destroy(gameObject);
        animator.SetBool("isPushed", false);
    }
    public void TakeDamage(Collider collider)
    {
        animationTimer = Time.fixedTime + 1.0f;
        animator.SetBool("isPushed", true);
        stats.Health -= collider.transform.GetComponent<CharacterStats>().CalcDamageAgainst(stats, UI_skillsManage.GetCurrentSkillInfo());
        if (stats.Health <= 0)
            animator.SetBool("isDead", true);
        transform.Find(UI_skillsManage.GetCurrentUIskillInfo().fileName + "_hit").GetComponent<ParticleSystem>().Play();
        UI_skillsManage.FinishSkillExecution();
    }
}
