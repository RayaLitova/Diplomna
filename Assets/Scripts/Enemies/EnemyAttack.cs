using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    public Transform particles;
    private Vector3 particlesStartPos;
    private EnemyAnimationController animationController;
    [SerializeField] string particlesName = "None";
    public bool isAttackingDisabled = false;

    private void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
        try
        {
            particles = transform.Find(particlesName);
            particlesStartPos = particles.localPosition;
            FinishExecution(); //set particles to inactive after getting the reference
        }
        catch (Exception) 
        {
            particles = null;
        }
    }
    
    public void StartExecution()
    {
        if (isAttackingDisabled)
            return;
        try
        {
            particles.gameObject.SetActive(true);
            animationController.AttackAnimation(true);
        }
        catch (Exception) { };
    }

    public void FinishExecution()
    {
        try
        {
            animationController.AttackAnimation(false);
        }
        catch (Exception) { };
            
        if (particles == null || !particles.gameObject.activeInHierarchy) //for optimization
            return;
        particles.transform.localPosition = particlesStartPos;
        particles.gameObject.SetActive(false);
    }
}
