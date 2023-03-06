using System.Collections;
using UnityEngine;
using System.Linq;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;
    private bool isMovingDisabled = true;
    private int pathNum = 1;
    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
        transform.position = GenerateDungeon.cameraPoints.ElementAt(0).transform.position;
        isMovingDisabled = false;
    }
    private void FixedUpdate()
    {
        if (pathNum == GenerateDungeon.cameraPoints.Capacity)
            return;

        Transform target = GenerateDungeon.cameraPoints.ElementAt(pathNum).transform;
        transform.position = Vector3.MoveTowards(transform.position, target.position, 7f);
        switch (GenerateDungeon.rotationList.ElementAt(pathNum))
        {
            case 1:
                transform.forward = Vector3.right;
                break;
            case 2:
                transform.forward = Vector3.back;
                break;
            case 3:
                transform.forward = Vector3.left;
                break;
            case 4:
                transform.forward = Vector3.forward;
                break;


        }
        //transform.rotation = Quaternion.LookRotation(target.position);
        if (Vector3.Distance(transform.position, target.position) < 30f)
            pathNum++;
        if (pathNum == GenerateDungeon.cameraPoints.Capacity) 
            StartCoroutine("WaitForLichAnimation");
    }

   /* private void FixedUpdate()
    {
        if (isMovingDisabled)
            return;

        foreach (GameObject path in GenerateDungeon.cameraPoints)
        {
            while (Vector3.Distance(transform.position, path.transform.position) > 30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, path.transform.position, 0.1f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, path.transform.rotation, 0.5f * Time.deltaTime);
            }
        }
        StartCoroutine("WaitForLichAnimation");
    }*/
    private IEnumerator WaitForLichAnimation()
    {
        isMovingDisabled = true;
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
