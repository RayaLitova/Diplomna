using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillExecution : MonoBehaviour
{
    public void ExecuteSkill()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius / 12f, LayerMask.GetMask("Enemies"));
        if (colliders.Length == 0)
            return;
        if (UI_skillsManage.GetCurrentSkillInfo().effectFlags.Contains("AOE"))
        {
            foreach (Collider collider in colliders)
            {
                Debug.Log("|AAA");
                collider.transform.GetComponent<EnemyTakeDamage>().TakeDamage(collider);
                SkillEffects.ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, collider);
            }
        }
        else
        {
            colliders[0].transform.GetComponent<EnemyTakeDamage>().TakeDamage(colliders[0]);
            SkillEffects.ApplyEffects(UI_skillsManage.GetCurrentSkillInfo().effectFlags, colliders[0]);
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GameObject.Find("Kgirls01").transform.Find("Center").position, GetComponent<CapsuleCollider>().radius / 5f);

    }
}
