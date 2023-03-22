using UnityEngine;
using System;

public class EnemyAnimationController : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected string hitAnimationVar;
    [SerializeField] protected string deathAnimationVar;
    [SerializeField] protected string attackAnimationVar;
    [SerializeField] protected string walkAnimationVar = "";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void takeDamageAnimation(bool isActive)
    {
        try
        {
            animator.SetBool(hitAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
    }
    public virtual void deathAnimation(bool isActive)
    {
        try
        {
            animator.SetBool(deathAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
    }

    public virtual void AttackAnimation(bool isActive)
    {
        animator.SetBool(attackAnimationVar, isActive);
        try
        {
            animator.SetBool(attackAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
    }

    public virtual void WalkAnimation(bool isActive)
    {
        try
        {
            if (walkAnimationVar != "")
                animator.SetBool(walkAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
    }
}
