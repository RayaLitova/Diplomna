using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
            GameObject.Find("Player").transform.position = GameObject.Find("BossRoomSpawn").transform.position;
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
            GameObject.Find("Player").transform.position = GameObject.Find("PortalRoomSpawn").transform.position;
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
        }
    }
}
