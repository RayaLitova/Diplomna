using UnityEngine;

public class SkillExecution : MonoBehaviour
{
    private Vector3 spherePosition;//for auto aim
    private float sphereRadius;
    public virtual void ExecuteSkill()
    {
        Transform character = transform.parent.Find("Center");
        spherePosition = transform.position; //setup sphere position
        sphereRadius = (GetComponent<CapsuleCollider>().radius / 4f) * transform.localScale.x; //setup sphere radius
        Collider[] colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies")); // check for enemies hit
        if (colliders.Length == 0)
        {
            spherePosition = transform.position + Vector3.left * ((transform.position.x - character.position.x) / 2); // place sphere to second priority
            sphereRadius = ((transform.position.x - character.position.x) / 2) + GetComponent<CapsuleCollider>().radius / 8f; // change radius
            colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies")); 
            if (colliders.Length == 0)
            {
                spherePosition = character.position; // place sphere to third priority
                sphereRadius = ((transform.position.x - character.position.x)) + GetComponent<CapsuleCollider>().radius / 8f; // change radius
                colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies"));
                if (colliders.Length == 0)
                    return;
            }
            transform.parent.LookAt(colliders[0].transform.position); // rotate character
        }
        
        colliders[0].transform.GetComponent<EnemyTakeDamage>().TakeDamage(GetComponentInParent<CharacterStats>());        
    }
}
