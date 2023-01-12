using UnityEngine;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (LoadDungeon.dungeonLevel == 10)
            Debug.Log("GAME OVER");

        LoadDungeon.dungeonLevel++;
        LoadScene.Load("CityScene");
    }
}
