using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skills_UI : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keyCodes;

    public static Dictionary<string, UI_Skill_Info> Skills;

    public static int SkillAnimationCount = 6;
    public static Dictionary<string, int> SkillAnimationIndex;
 
    private static string skillExecuted = null;

    private void Awake()
    {
        SkillAnimationIndex = new Dictionary<string, int>()
        {
            {"FirePunch", 0},
            { "punch_2", 1},
        };

        Skills = new Dictionary<string, UI_Skill_Info>()
        {
            {"Action key 1", null},
            {"Action key 2", null},
            {"Action key 3", null},
        };

        keyCodes = new Dictionary<string, KeyCode>()
        {
            {"Action key 1", KeyCode.Q },
            { "Action key 2", KeyCode.E},
            { "Action key 3", KeyCode.R},
        };
    }
    void Update()
    {
        if (Input.GetKeyDown(keyCodes["Action key 1"]))
        {
            try
            {
                Skills["Action key 1"].Execute();
                skillExecuted = "Action key 1";
            }
            catch (NullReferenceException) { };
        }
        else if (Input.GetKeyDown(keyCodes["Action key 2"]))
        {
            try
            {
                Skills["Action key 2"].Execute();
                skillExecuted = "Action key 2";
            }
            catch (NullReferenceException) { };
        }
        else if (Input.GetKeyDown(keyCodes["Action key 3"]))
        {
            try
            {
                Skills["Action key 3"].Execute();
                skillExecuted = "Action key 3";
            }
            catch (NullReferenceException) { };
        }
    }

    public static void FinishSkillExecution(bool isEnemyHit = true)
    {
        if (skillExecuted != null)
            Skills[skillExecuted].FinishExecution(isEnemyHit);
    }

    public static Skill_info GetCurrentSkill()
    {
        return Skills[skillExecuted].gameObject.GetComponent<Skill_info>();
    }


}
