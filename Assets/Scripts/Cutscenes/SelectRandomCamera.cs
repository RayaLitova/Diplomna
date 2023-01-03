using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomCamera : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

}
