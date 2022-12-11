using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected string hitAnimationVar;
    [SerializeField] protected string deathAnimationVar;
    [SerializeField] protected string attackAnimationVar;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void takeDamageAnimation(bool isActive)
    {
        animator.SetBool(hitAnimationVar, isActive);
    }
    public virtual void deathAnimation(bool isActive)
    {
        animator.SetBool(deathAnimationVar, isActive);
    }

    public virtual void AttackAnimation(bool isActive)
    {
        animator.SetBool(attackAnimationVar, isActive);
    }
}
