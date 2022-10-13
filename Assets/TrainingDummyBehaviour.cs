using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TrainingDummyBehaviour : MonoBehaviour
{
    private Animator animator;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if(collision.collider.gameObject.name == "Kgirls01")
        {
            Debug.Log(health);
            animator.SetBool("isPushed", true);
            health -= 10;
            if (health <= 0)
                animator.SetBool("isDead", true);
            StaticFunctions.timer(3);
            if (health <= 0)
                Destroy(gameObject);
            animator.SetBool("isPushed", false);
        }
    }
}
