using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform particles;
    private Vector3 particlesStartPos;
    private EnemyAnimationController animationController;
    [SerializeField] string particlesName;

    private void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
        particles = transform.Find(particlesName);
        FinishExecution(); //set particles to inactive after getting the reference
        particlesStartPos = particles.localPosition;
    }
    
    public void StartExecution()
    {
        particles.gameObject.SetActive(true);
        animationController.AttackAnimation(true);
    }

    public void FinishExecution()
    {
        if (!particles.gameObject.activeInHierarchy) //optimization
            return;

        particles.transform.localPosition = particlesStartPos;
        particles.gameObject.SetActive(false);
        animationController.AttackAnimation(false);
    }
}
