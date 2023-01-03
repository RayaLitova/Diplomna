using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (LoadDungeon.dungeonLevel == 10)
            Debug.Log("GAME OVER");
        LoadDungeon.dungeonLevel++;
        SceneManager.LoadScene("CityScene");
    }
}
