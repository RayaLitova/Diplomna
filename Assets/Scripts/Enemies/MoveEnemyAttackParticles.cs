using UnityEngine;

public class MoveEnemyAttackParticles : MonoBehaviour
{
    void Update()
    {
        transform.localPosition += Vector3.forward * 0.0001f;
    }
}
