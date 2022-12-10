using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        if (StaticFunctions.dungeonLevel == 1)
            Debug.Log("GAME OVER");
        SceneManager.LoadScene("CityScene");
    }
}
