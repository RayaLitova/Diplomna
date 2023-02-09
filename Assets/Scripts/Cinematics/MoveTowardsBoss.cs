using System.Collections;
using UnityEngine;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;
    private Transform path;
    private int childNum = 0;
    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
        path = GameObject.Find("StartingCutscenePath").transform;
    }

    private void FixedUpdate()
    {
        if (childNum == path.childCount)
            return;

        Transform target = path.GetChild(childNum);
        transform.position = Vector3.MoveTowards(transform.position, target.position, 5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 30f)
            childNum++;
        if (childNum == path.childCount)
            StartCoroutine("WaitForLichAnimation");
    }

    private IEnumerator WaitForLichAnimation()
    {
        transform.LookAt(boss.position, Vector3.up);
        yield return new WaitForSeconds(3f);
        GetComponent<FinishCutscene>().StopCutscene();
    }

    private void OnDisable()
    {
        Destroy(this); 
    }

}
