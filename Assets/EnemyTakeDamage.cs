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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Skill")
        {
            if (transform.Find(Skills_UI.GetCurrentUIskillInfo().fileName + "_hit").GetComponent<ParticleSystem>().isPlaying)
                return;

            TakeDamage(collision);
        }
    }

    public void TakeDamage(Collision collision)
    {

        animator.SetBool("isPushed", true);
        stats.Health -= collision.collider.transform.parent.GetComponent<CharacterStats>().CalcDamageAgainst(stats, Skills_UI.GetCurrentSkillInfo());
        Debug.Log(stats.Health);
        if (stats.Health <= 0)
            animator.SetBool("isDead", true);
        animationTimer = Time.fixedTime + 1.0f;
        transform.Find(Skills_UI.GetCurrentUIskillInfo().fileName + "_hit").GetComponent<ParticleSystem>().Play();
        Skills_UI.FinishSkillExecution();
    }
}
