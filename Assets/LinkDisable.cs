using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkDisable : MonoBehaviour
{
    [SerializeField] GameObject linked;
    void Update()
    {
        if (!linked.activeInHierarchy)
            gameObject.SetActive(false);
    }
}
