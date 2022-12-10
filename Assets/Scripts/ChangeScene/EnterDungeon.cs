using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : InteractAction
{
    public override void Action()
    {
        SceneManager.LoadScene("DungeonScene");
    }
}
