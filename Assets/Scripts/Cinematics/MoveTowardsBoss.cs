using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;
    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, boss.position) > 100)
            transform.position = Vector3.MoveTowards(transform.position, boss.position, 5f);
        else
            StartCoroutine("WaitForLichAnimation");
    }

    private IEnumerator WaitForLichAnimation()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<FinishCutscene>().StopCutscene();
    }

    private void OnDisable()
    {
        Destroy(this);
    }

}
