using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string hitAnimationVar;
    [SerializeField] string deathAnimationVar;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void takeDamageAnimation(bool isActive)
    {
        animator.SetBool(hitAnimationVar, isActive);
    }
    public void deathAnimation(bool isActive)
    {
        animator.SetBool(deathAnimationVar, isActive);
    }
}
