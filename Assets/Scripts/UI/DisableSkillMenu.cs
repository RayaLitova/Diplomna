using UnityEngine;

public class DisableSkillMenu : MonoBehaviour
{
    private GetCurrentRoom rooms;

    private void Start()
    {
        rooms = GameObject.Find("Rooms").GetComponent<GetCurrentRoom>();  
    }
    private void Update()
    {
        if (rooms.CheckRooms(transform) == null)
            return;

        Destroy(GameObject.Find("SkillMenu"));
        Destroy(this);
    }
}
