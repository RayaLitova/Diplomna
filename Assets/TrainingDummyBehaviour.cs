using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TrainingDummyBehaviour : MonoBehaviour
{
    private Animator animator;
    private float health = 100;

    private float animationTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > animationTimer)
        {
            if (health <= 0)
            {
                animationTimer += 5;
                health = 100;
                return;
            }
            animator.SetBool("isDead", false);
            animator.SetBool("isPushed", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name == "Kgirls01")
        {
            animator.SetBool("isPushed", true);
            health -= 10;
            Debug.Log(health);
            if (health <= 0)
                animator.SetBool("isDead", true);
            animationTimer = Time.fixedTime + 1.0f;
            
        }
    }
}
