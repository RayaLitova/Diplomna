using UnityEngine;
using UnityEngine.UI;

public class Gather : InteractAction
{
    private CharacterAnimationController ac;
    private void Start()
    {
        string name = StaticFunctions.RemoveClones(gameObject.name);
        name = StaticFunctions.AddWhitespaces(name);
        description = "Gather " + name;
        ac = GameObject.Find("Player").GetComponent<CharacterAnimationController>();
    }
    public override void Action()
    {
        GameObject room = GetCurrentRoom.CheckRooms(transform);
        if (room != null && room.GetComponent<CheckRoom>().EnemiesInRoom()) //portal room is room == null
            return;

        GameObject.Find("Scripts").GetComponent<DungeonObjectives>().UpdateHerbProgress();
        Herb herb = Resources.Load<Herb>("HerbItems/" + StaticFunctions.RemoveClones(gameObject.name));

        if (!SavingManager.gameData.Items.ContainsKey("HerbItems"))
            SavingManager.gameData.Items["HerbItems"] = new();
        if (!SavingManager.gameData.Items["HerbItems"].ContainsKey(herb.name))
            SavingManager.gameData.Items["HerbItems"][herb.name] = 0;

        SavingManager.gameData.Items["HerbItems"][herb.name] += Random.Range(1, 4);
        FinishGathering.herb = gameObject;
        Transform herbSlot = GameObject.Find("HerbBag").transform.GetChild(0).GetChild(0);
        if (!herbSlot.gameObject.activeInHierarchy)
            UpdateCounts.update = true;
        else
            herbSlot.GetComponent<FindHerbSlot>().FindSlot(herb).Find("Count").GetComponent<Text>().text = SavingManager.gameData.Items["HerbItems"][herb.name].ToString();
        ac.Gather(true);
    }
}