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
        {
            Destroy(gameObject);
        }
        animator.SetBool("isPushed", false);
        
    }

    private void OnCollisionEnter(Collision collision)//move to skill
    {
        if (collision.collider.gameObject.tag == "Skill")
        {
            TakeDamage(collision);
        }
    }

    public void TakeDamage(Collision collision)
    {
        animator.SetBool("isPushed", true);
        stats.Health -= collision.collider.transform.parent.GetComponent<CharacterStats>().CalcDamageAgainst(stats, UI_skillsManage.GetCurrentSkillInfo());
        if (stats.Health <= 0)
            animator.SetBool("isDead", true);
        animationTimer = Time.fixedTime + 1.0f;
        transform.Find(UI_skillsManage.GetCurrentUIskillInfo().fileName + "_hit").GetComponent<ParticleSystem>().Play();
        UI_skillsManage.FinishSkillExecution();
    }
}
