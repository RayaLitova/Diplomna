using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStats : MonoBehaviour
{
    public int damage;
    public float crit;
    public int missChance; 
    public string[] effectFlags;
    public int rage;
    public bool isEnraged = false;

    public void EnterRagedMode()
    {
        Debug.Log("Enter raged mode");
        isEnraged = true;
        damage *= 2;
        crit *= 2;
        GetComponent<EnemyNavMeshAgentFollow>().walkSpeedMultiplier = 2f;
        StartCoroutine("RageDecrease");
    }

    public void ExitRagedMode()
    {
        Debug.Log("Exit raged mode");
        isEnraged = false;
        damage /= 2;
        crit /= 2;
        GetComponent<EnemyNavMeshAgentFollow>().walkSpeedMultiplier = 1f;
    }

    IEnumerator RageDecrease()
    {
        while (rage >= 0)
        {
            rage--;
            yield return new WaitForSeconds(0.1f);
        }
        ExitRagedMode();
    }
}
