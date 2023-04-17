using UnityEngine;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (SavingManager.gameData.dungeonLevel == 10)
            Debug.LogError("GAME OVER");

        if (DungeonObjectives.completedObjectivesCount == DungeonObjectives.objectivesCount)
        {
            SavingManager.gameData.dungeonLevel++;
            Debug.LogError("Level Passed");
        }
        LoadScene.Load("CityScene");
    }
}
