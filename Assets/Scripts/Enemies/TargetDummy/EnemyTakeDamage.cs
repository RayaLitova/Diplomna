using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Animator animator;
    private float animationTimer;
    private CharacterStats stats;
    private EnemyAnimationController animationController;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<EnemyAnimationController>();
    }

    void Update()
    {
        if (Time.fixedTime < animationTimer)
            return;
        if (stats.Health <= 0)
            Destroy(gameObject);
        animationController.takeDamageAnimation(false);
    }
    public void TakeDamage(Collider collider)
    {
        animationTimer = Time.fixedTime + 1.0f;
        animationController.takeDamageAnimation(true);
        stats.Health -= collider.transform.GetComponent<CharacterStats>().CalcDamageAgainst(stats, UI_skillsManage.GetCurrentSkillInfo());
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
