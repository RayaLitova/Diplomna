using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyAttackParticles : MonoBehaviour
{

    void Update()
    {

        transform.localPosition += Vector3.forward * 0.0001f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;
        collision.collider.GetComponent<CharacterTakeDamage>().TakeDamage(transform.parent);
        gameObject.SetActive(false);
    }
}
