using System.Collections;
using UnityEngine;
using System.Linq;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;
    private Vector3 targetRotation = Vector3.zero;
    private float angle = 0f;
    private int pathNum = 1;
    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
        transform.position = GenerateDungeon.cameraPoints.ElementAt(0).transform.position;
    }
    private void FixedUpdate()
    {
        if (targetRotation != Vector3.zero && transform.forward != targetRotation)
        {
            transform.rotation = Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up);
            return;
        }

        if (pathNum == GenerateDungeon.cameraPoints.Count)
            return;

        Transform target = GenerateDungeon.cameraPoints.ElementAt(pathNum).transform;
        transform.position = Vector3.MoveTowards(transform.position, target.position, 7f);
        switch (GenerateDungeon.rotationList.ElementAt(pathNum - 1))
        {
            case 1:
                //transform.forward = Vector3.back;
                targetRotation = Vector3.back;
                break;
            case 2:
                targetRotation = Vector3.left;
                //transform.forward = Vector3.left;
                break;
            case 3:
                targetRotation = Vector3.forward;
                //transform.forward = Vector3.forward;
                break;
            case 4:
                targetRotation = Vector3.right;
                //transform.forward = Vector3.right;
                break;
        }
        angle = Vector3.Angle(transform.forward, targetRotation);
        //transform.rotation = Quaternion.LookRotation(target.position);
        if (Vector3.Distance(transform.position, target.position) < 30f)
            pathNum++;
        if (pathNum == GenerateDungeon.cameraPoints.Count) 
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
        targetRotation = Vector3.zero;
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
