using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDungeon : InteractAction
{
    public override void Action()
    {
        SceneManager.LoadScene("CityScene");
    }
}
