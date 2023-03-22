using System.Collections;
using UnityEngine;

public class SkillHitCharacter : MonoBehaviour
{
    private bool isInRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        isInRange = true;
        StartCoroutine("HitCharacter", other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        isInRange=false;
    }

    private IEnumerator HitCharacter(Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        if (!isInRange)
            yield break;
        other.GetComponent<CharacterTakeDamage>().TakeDamage(transform.parent);
        gameObject.SetActive(false);
    }
}
