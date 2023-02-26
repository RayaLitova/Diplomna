using System.Collections;
using UnityEngine;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;
    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
        transform.position = GenerateDungeon.cameraPoints[0].transform.position;
        foreach (GameObject path in GenerateDungeon.cameraPoints)
        {
            while (Vector3.Distance(transform.position, path.transform.position) > 30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, path.transform.position, 5f);
                transform.rotation = Quaternion.Slerp(transform.rotation, path.transform.rotation, 0.5f * Time.deltaTime);
            }
        }
        StartCoroutine("WaitForLichAnimation");
    }
    /*private void FixedUpdate()
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
    }*/

    private IEnumerator WaitForLichAnimation()
    {
        transform.LookAt(boss.position, Vector3.up);
        yield return new WaitForSeconds(3f);
        GetComponent<FinishCutscene>().StopCutscene();
    }

    private void OnDisable()
    {
        Destroy(GameObject.Find("Scripts").GetComponent<GenerateDungeon>());
        Destroy(this); 
    }

}
