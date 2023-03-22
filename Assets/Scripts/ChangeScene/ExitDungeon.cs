using UnityEngine;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (GenerateDungeon.dungeonLevel == 10)
            Debug.Log("GAME OVER");

        if (DungeonObjectives.completedObjectivesCount == DungeonObjectives.objectivesCount)
        {
            GenerateDungeon.dungeonLevel++;
            Debug.Log("Level Passed");
        }
        LoadScene.Load("CityScene");
    }
}
