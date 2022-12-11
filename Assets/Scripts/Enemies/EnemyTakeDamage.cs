using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private float animationTimer;
    private CharacterStats stats;
    private EnemyAnimationController animationController;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        animationController = GetComponent<EnemyAnimationController>();
    }

    void Update()
    {
        if (Time.fixedTime < animationTimer)
            return;
        animationController.takeDamageAnimation(false);
    }
    public void TakeDamage(CharacterStats enemyStats)
    {
        animationTimer = Time.fixedTime + 1.0f;
        animationController.takeDamageAnimation(true);
        stats.Health -= enemyStats.CalcDamageAgainst(stats, UI_skillsManage.GetCurrentSkillInfo());
        if (stats.Health <= 0)
            animationController.deathAnimation(true);
        transform.Find(UI_skillsManage.GetCurrentUIskillInfo().fileName + "_hit").GetComponent<ParticleSystem>().Play();
        UI_skillsManage.FinishSkillExecution();
    }

    public void TakeDOTdamage(int damage)
    {
        animationController.takeDamageAnimation(true);
        stats.Health -= damage;
        if (stats.Health <= 0)
            animationController.deathAnimation(true);
        //play burn particles
    }
}
