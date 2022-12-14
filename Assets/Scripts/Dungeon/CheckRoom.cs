using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CheckRoom : MonoBehaviour
{
    private Transform[] ObjectsInRoom = new Transform[20];
    private int objectCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "BackgroundObjects" && !Array.Exists(ObjectsInRoom, element => element == other.transform))
        {
            
            ObjectsInRoom[objectCount] = other.transform; 
            objectCount++;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ObjectsInRoom = ObjectsInRoom.Where(val => val != other.transform).ToArray();
            objectCount--;
        }
    }
    public bool CheckForObject(Transform target)
    {
        if (ObjectsInRoom.Contains(target))
            return true;
        return false;
    }

    public int GetNumberOfTags(string searchedTag)
    {
        int count = 0;
        foreach (Transform curr in ObjectsInRoom)
        {
            if (curr != null && curr.tag == searchedTag)
                count++;
        }
        return count;
    }
    
}
