using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFloorNumber : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Text>().text = LoadDungeon.dungeonLevel.ToString();
    }

    
}
