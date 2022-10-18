using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField] private float health;

    private Animator animator;
    private float animationTimer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.fixedTime < animationTimer) 
            return;
        
        if (health <= 0)
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
            health -= 10;
            Debug.Log(health);
            if (health <= 0)
                animator.SetBool("isDead", true);
            animationTimer = Time.fixedTime + 1.0f;
            Destroy(collision.collider.gameObject);
        }
    }
}
