using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentRoom : MonoBehaviour
{
    private static Transform rooms;

    private void Start()
    {
        rooms = transform;
    }

    public static string CheckRooms(Transform target)
    {
        foreach (Transform room in rooms)
        {
            if (room.GetComponent<CheckRoom>().CheckForObject(target))
                return room.gameObject.name;
        }
        return null;
    }
}
