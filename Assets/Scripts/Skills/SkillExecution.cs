using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillExecution : MonoBehaviour
{
    private Vector3 spherePosition;//for auto aim
    private float sphereRadius;
    public void ExecuteSkill()
    {
        Transform character = transform.parent.Find("Center");
        spherePosition = transform.position; //setup sphere position
        sphereRadius = GetComponent<CapsuleCollider>().radius / 8f; //setup sphere radius
        Collider[] colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies")); // check for enemies hit
        if (colliders.Length == 0)
        {
            spherePosition = transform.position + Vector3.left * ((transform.position.x - character.position.x) / 2); // place sphere to second priority
            sphereRadius = ((transform.position.x - character.position.x) / 2) + GetComponent<CapsuleCollider>().radius / 8f; // change radius
            colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies")); 
            if (colliders.Length == 0)
            {
                spherePosition = character.position; // plache sphere to third priority
                sphereRadius = ((transform.position.x - character.position.x)) + GetComponent<CapsuleCollider>().radius / 8f; // change radius
                colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies"));
                if (colliders.Length == 0)
                    return;
            }
            transform.parent.LookAt(colliders[0].transform.position); // rotate character
        }
        if (UI_skillsManage.GetCurrentSkillInfo().effectFlags.Contains("AOE")) // hit multiple enemies
        {
            foreach (Collider collider in colliders)
            {
                collider.transform.GetComponent<EnemyTakeDamage>().TakeDamage(collider);
                UI_skillsManage.GetCurrentSkillEffects().ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, collider);
            }
        }
        else // hit one enemy
        {
            colliders[0].transform.GetComponent<EnemyTakeDamage>().TakeDamage(colliders[0]);
            UI_skillsManage.GetCurrentSkillEffects().ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, colliders[0]);
        } 
    }
}
