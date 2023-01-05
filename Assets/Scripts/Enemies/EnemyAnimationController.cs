using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void WalkAnimation(bool isActive)
    {
        Debug.Log(animator.GetBool(attackAnimationVar) + " " + animator.GetBool(hitAnimationVar));
        if(walkAnimationVar != "")
            animator.SetBool(walkAnimationVar, isActive);
    }
}
