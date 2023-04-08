using UnityEngine;
using System;

public class EnemyAnimationController : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected string hitAnimationVar;
    [SerializeField] protected string deathAnimationVar;
    [SerializeField] protected string attackAnimationVar;
    [SerializeField] protected string walkAnimationVar = "";
    private EnemySoundController soundController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        soundController = GetComponent<EnemySoundController>();
    }
    public virtual void takeDamageAnimation(bool isActive)
    {
        try
        {
            animator.SetBool(hitAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
        if (gameObject.tag == "Boss")
            soundController.PlayTakeDamageSound();
    }
    public virtual void deathAnimation(bool isActive)
    {
        try
        {
            animator.SetBool(deathAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
        if (gameObject.tag == "Boss")
            soundController.PlayDeathSound();
    }

    public virtual void AttackAnimation(bool isActive)
    {
        try
        {
            animator.SetBool(attackAnimationVar, isActive);
        }
        catch (NullReferenceException) { };
        if (gameObject.tag == "Boss")
            soundController.PlayAttackSound();
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
