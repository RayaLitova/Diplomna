using UnityEngine;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (GenerateDungeon.dungeonLevel == 10)
            Debug.Log("GAME OVER");

        GenerateDungeon.dungeonLevel++;
        LoadScene.Load("CityScene");
    }
}
