using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsBoss : MonoBehaviour
{
    private Transform boss;

    [SerializeField] private GameObject mainCamera;

    private void Start()
    {
        boss = GameObject.Find("Lich").transform;
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
        mainCamera.SetActive(true);
        gameObject.SetActive(false);
    }

}
