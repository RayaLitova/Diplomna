using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementText : MonoBehaviour
{
    int i = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Text>().text = i.ToString();
            i++;
        }
    }
}
