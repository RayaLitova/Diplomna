using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationController : EnemyAnimationController
{
    [SerializeField] string hitAnimationNumberVar;
    [SerializeField] int hitAnimationCount;

    [SerializeField] string deathAnimationNumberVar;
    [SerializeField] int deathAnimationCount;

    [SerializeField] string attackAnimationNumberVar;
    [SerializeField] int attackAnimationCount;
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("IdleEventHandler");
    }

    IEnumerator IdleEventHandler()
    {
        if (!animator.GetBool("isInIdleEvent"))
            animator.SetBool("isInIdleEvent", true);
        yield return new WaitForSeconds(Random.Range(5f, 10f));
    }

    public override void deathAnimation(bool isActive)
    {
        base.deathAnimation(isActive);
        if (isActive)
            animator.SetFloat(deathAnimationNumberVar, (1 / deathAnimationCount) * Random.Range(0, deathAnimationCount + 1));

    }

    public override void takeDamageAnimation(bool isActive)
    {
        base.takeDamageAnimation(isActive);
        if (isActive)
            animator.SetFloat(hitAnimationNumberVar, (1 / hitAnimationCount) * Random.Range(0, hitAnimationCount + 1));
    }

    public override void AttackAnimation(bool isActive)
    {
        base.AttackAnimation(isActive);
        if (isActive)
            animator.SetFloat(attackAnimationNumberVar, (1 / attackAnimationCount) * Random.Range(0, attackAnimationCount + 1));

    }
}
