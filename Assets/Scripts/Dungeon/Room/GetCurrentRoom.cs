using UnityEngine;

public class GetCurrentRoom : MonoBehaviour
{
    public static GameObject CheckRooms(Transform target)
    {
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if (room.GetComponent<CheckRoom>().CheckForObject(target))
                return room;
        }
        return null;
    }
}
