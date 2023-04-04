using UnityEngine;

public class Gather : InteractAction
{
    private GetCurrentRoom rooms;
    private CharacterAnimationController ac;
    private void Start()
    {
        description = "Gather " + gameObject.name;
        rooms = GameObject.Find("Scripts").GetComponent<GetCurrentRoom>();
        ac = GameObject.Find("Player").GetComponent<CharacterAnimationController>();
    }
    public override void Action()
    {
        GameObject room = rooms.CheckRooms(transform);
        if (room != null && room.GetComponent<CheckRoom>().GetNumberOfTags("Enemy") != 0)
            return;

        FinishGathering.herb = gameObject;
        ac.Gather(true);
    }
}
