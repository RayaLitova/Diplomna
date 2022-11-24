using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkillExecution : MonoBehaviour
{
    private Vector3 spherePosition;
    private float sphereRadius;
    public void ExecuteSkill() //to do: finish auto aim
    {
        Transform character = transform.parent.Find("Center");
        spherePosition = transform.position;
        sphereRadius = GetComponent<CapsuleCollider>().radius / 8f;
        Collider[] colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies"));
        if (colliders.Length == 0)
        {
            Debug.Log("Start second priority search");
            spherePosition = transform.position + Vector3.left * ((transform.position.x - character.position.x) / 2);
            sphereRadius = ((transform.position.x - character.position.x) / 2) + GetComponent<CapsuleCollider>().radius / 8f;
            colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies"));
            if (colliders.Length == 0)
            {
                Debug.Log("Start third priority search");
                spherePosition = character.position;
                sphereRadius = ((transform.position.x - character.position.x)) + GetComponent<CapsuleCollider>().radius / 8f;
                colliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Enemies"));
                if (colliders.Length == 0)
                {
                    Debug.Log("Search failed");
                    return;
                }
            }
            Debug.Log("Search successful");
            transform.parent.LookAt(colliders[0].transform.position);
        }
        Debug.Log("Hit");
        if (UI_skillsManage.GetCurrentSkillInfo().effectFlags.Contains("AOE"))
        {
            foreach (Collider collider in colliders)
            {
                collider.transform.GetComponent<EnemyTakeDamage>().TakeDamage(collider);
                UI_skillsManage.GetCurrentSkillEffects().ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, collider);
            }
        }
        else
        {
            Debug.Log(UI_skillsManage.GetCurrentSkillInfo().effectFlags.Length);
            colliders[0].transform.GetComponent<EnemyTakeDamage>().TakeDamage(colliders[0]);
            UI_skillsManage.GetCurrentSkillEffects().ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, colliders[0]);
        } 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spherePosition, sphereRadius);
        //Gizmos.DrawSphere(transform.position, GetComponent<CapsuleCollider>().radius / 8f);
        //Gizmos.DrawSphere(transform.position + Vector3.right * ((transform.position.x - transform.parent.Find("Center").position.x) / 2),
        //                  ((transform.position.x - transform.parent.Find("Center").position.x) / 2) + GetComponent<CapsuleCollider>().radius / 8f);
        //Gizmos.DrawSphere(transform.parent.Find("Center").position,
        //                 ((transform.position.x - transform.parent.Find("Center").position.x)) + GetComponent<CapsuleCollider>().radius / 8f);
    }
}
