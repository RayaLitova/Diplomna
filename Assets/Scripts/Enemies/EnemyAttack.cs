using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartExecution()
    {
        animator.SetBool("isAttacking", true);
        Debug.Log("Start execution");
    }

    public void FinishExecution()
    {
        animator.SetBool("isAttacking", false);
        Debug.Log("Finish execution");
    }
}
