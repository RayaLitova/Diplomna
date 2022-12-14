using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHitCharacter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;
        collision.collider.GetComponent<CharacterTakeDamage>().TakeDamage(transform.parent);
        gameObject.SetActive(false);
    }
}
