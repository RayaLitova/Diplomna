using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    public Transform particles;
    private Vector3 particlesStartPos;
    private EnemyAnimationController animationController;
    [SerializeField] string particlesName;
    public bool isAttackingDisabled = false;

    private void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
        particles = transform.Find(particlesName);
        particlesStartPos = particles.localPosition;
        FinishExecution(); //set particles to inactive after getting the reference
    }
    
    public void StartExecution()
    {
        if (isAttackingDisabled)
            return;
        particles.gameObject.SetActive(true);
        animationController.AttackAnimation(true);
    }

    public void FinishExecution()
    {
        try
        {
            animationController.AttackAnimation(false);
        }
        catch (NullReferenceException) { };
            
        if (particles == null || !particles.gameObject.activeInHierarchy) //for optimization
            return;
        particles.transform.localPosition = particlesStartPos;
        particles.gameObject.SetActive(false);
    }
}
