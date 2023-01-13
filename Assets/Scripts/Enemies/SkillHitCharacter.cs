using UnityEngine;

public class SkillHitCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        other.GetComponent<CharacterTakeDamage>().TakeDamage(transform.parent);
        gameObject.SetActive(false);
    }
}
