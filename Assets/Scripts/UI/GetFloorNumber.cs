using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFloorNumber : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = LoadDungeon.dungeonLevel.ToString();
    }

    
}
