using System.Collections;
using UnityEngine;

public class SkeletonAnimationController : EnemyAnimationController
{
    [SerializeField] string hitAnimationNumberVar;
    [SerializeField] int hitAnimationCount;

    [SerializeField] string deathAnimationNumberVar;
    [SerializeField] int deathAnimationCount;

    [SerializeField] string attackAnimationNumberVar;
    [SerializeField] int attackAnimationCount;

    private SkillStats stats;
    private void Start()
    {
        stats = GetComponent<SkillStats>();
        animator = GetComponent<Animator>();
        StartCoroutine("IdleEventHandler");
    }

    IEnumerator IdleEventHandler()
    {
        while (true)
        {
            if (!animator.GetBool("isInIdleEvent"))
                animator.SetBool("isInIdleEvent", true);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    public override void deathAnimation(bool isActive)
    {
        base.deathAnimation(isActive);
        if (isActive)
            //Choose random death animation
            animator.SetFloat(deathAnimationNumberVar, (1 / deathAnimationCount) * Random.Range(0, deathAnimationCount + 1));
    }

    public override void takeDamageAnimation(bool isActive)
    {
        base.takeDamageAnimation(isActive);
        if (isActive)
            //Choose random take damage animation
            animator.SetFloat(hitAnimationNumberVar, (1 / hitAnimationCount) * Random.Range(0, hitAnimationCount + 1));
    }

    public override void AttackAnimation(bool isActive)
    {
        base.AttackAnimation(isActive);
        if (isActive)
        {
            //Choose random attack animation
            animator.SetFloat(attackAnimationNumberVar, (1 / attackAnimationCount) * Random.Range(0, attackAnimationCount + 1));
            if (!stats.isEnraged)
            {
                stats.rage += Random.Range(10, 50); 
                if (stats.rage >= 100)
                    stats.EnterRagedMode();
            }
        }
    }

    public override void WalkAnimation(bool isActive)
    {
        base.WalkAnimation(isActive);
        if(!stats.isEnraged)
            animator.SetFloat("Rage", 0);
        else
            animator.SetFloat("Rage", 1);
    }
}
