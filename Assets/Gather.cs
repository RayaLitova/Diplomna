using UnityEngine;

public class Gather : InteractAction
{
    private GetCurrentRoom rooms;
    private CharacterAnimationController ac;
    private void Start()
    {
        string name = StaticFunctions.RemoveClones(gameObject.name);
        name = StaticFunctions.AddWhitespaces(name);
        description = "Gather " + name;
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
