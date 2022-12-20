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
        particles = transform.Find(particlesName);
        particlesStartPos = particles.localPosition;
        animationController = GetComponent<EnemyAnimationController>();
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
