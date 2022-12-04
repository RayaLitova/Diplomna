using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform particles;
    private Vector3 particlesStartPos;

    private Animator animator;
    private void Start()
    {
        particles = transform.Find("VoidBall_particles");
        animator = GetComponent<Animator>();
        particlesStartPos = particles.localPosition;
    }
    
    public void StartExecution()
    {
        particles.gameObject.SetActive(true);
        animator.SetBool("isAttacking", true);
    }

    public void FinishExecution()
    {
        particles.transform.localPosition = particlesStartPos;
        particles.gameObject.SetActive(false);
        animator.SetBool("isAttacking", false);
    }
}
