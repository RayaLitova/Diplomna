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
            animator.SetBool("isPushed", true);
            stats.Health -= collision.collider.transform.parent.GetComponent<CharacterStats>().CalcDamageAgainst(stats) ;
            Debug.Log(collision.collider.transform.parent.GetComponent<CharacterStats>().CalcDamageAgainst(stats));
            if (stats.Health <= 0)
                animator.SetBool("isDead", true);
            animationTimer = Time.fixedTime + 1.0f;
            Destroy(collision.collider.gameObject);
        }
    }
}
