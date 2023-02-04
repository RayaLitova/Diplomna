using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_skillsManage : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keyCodes;

    public static Dictionary<string, UI_Skill_Execution> Skills;

    public static int SkillAnimationCount = 6;
    public static Dictionary<string, int> SkillAnimationIndex;

    public RectTransform[] skillSlotTransform;
    public Dictionary<string, Vector3> SkillSlotAnchoredPosition;

    private static string skillExecuted = null;
    private static string lastSkillExecuted = null;

    public static float timeBetweenSkills = 2f;


    private void Awake()
    {
        SkillSlotAnchoredPosition = new Dictionary<string, Vector3>() { 
            { "Action key 1", skillSlotTransform[0].anchoredPosition },
            { "Action key 2", skillSlotTransform[1].anchoredPosition },
            { "Action key 3", skillSlotTransform[2].anchoredPosition },
        };
     
        SkillAnimationIndex = new Dictionary<string, int>()
        {
            {"FirePunch", 0},
            {"InfernalPunch", 1},
            {"Buff", 2},
            {"FireRain", 3},
            {"FireHurricane", 4},
        };

        Skills = new Dictionary<string, UI_Skill_Execution>()
        {
            {"Action key 1", null},
            {"Action key 2", null},
            {"Action key 3", null},
        };

        keyCodes = new Dictionary<string, KeyCode>()
        {
            { "Action key 1", KeyCode.Q },
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
                skillExecuted = "Action key 1";
                lastSkillExecuted = skillExecuted;
                Skills["Action key 1"].StartExecution();
            }
            catch (NullReferenceException) { };
        }
        else if (Input.GetKeyDown(keyCodes["Action key 2"]))
        {
            try
            {
                skillExecuted = "Action key 2";
                lastSkillExecuted = skillExecuted;
                Skills["Action key 2"].StartExecution();
            }
            catch (NullReferenceException) { };
        }
        else if (Input.GetKeyDown(keyCodes["Action key 3"]))
        {
            try
            {
                skillExecuted = "Action key 3";
                lastSkillExecuted = skillExecuted;
                Skills["Action key 3"].StartExecution();
            }
            catch (NullReferenceException) { };
        }
    }

    public static void FinishSkillExecution()
    {
        if (skillExecuted != null)
            Skills[skillExecuted].FinishExecution();
        skillExecuted = null;
    }

    public static SkillStats GetCurrentSkillInfo()
    {
        if (skillExecuted == null)
            if (lastSkillExecuted == null)
                return null;
            else
                return Skills[lastSkillExecuted].gameObject.GetComponent<SkillStats>();

        return Skills[skillExecuted].gameObject.GetComponent<SkillStats>();
    }

    public static UI_Skill_Execution GetCurrentUIskillInfo()
    {
        if (skillExecuted == null)
            return null;
        return Skills[skillExecuted]; 
    }

    public static UI_Skill_Execution GetLastUIskillInfo()
    {
        if (lastSkillExecuted == null)
            return null;
        return Skills[lastSkillExecuted];
    }

    public string getClosestSkillSlot(Vector3 skillPosition) // for skill position change
    {
        float min = 115.0f;
        string returnValue = null;
        float tmp = Vector3.Distance(skillPosition, skillSlotTransform[0].position);
        if (tmp < min)
        {
            min = tmp;
            returnValue = "Action key 1";
        }
        tmp = Vector3.Distance(skillPosition, skillSlotTransform[1].position);
        if (tmp < min)
        {
            min = tmp;
            returnValue = "Action key 2";
        }
        tmp = Vector3.Distance(skillPosition, skillSlotTransform[2].position);
        if (tmp < min)
            returnValue = "Action key 3";
        
        return returnValue;
    }

    public static SkillEffects GetCurrentSkillEffects()
    {
        if (skillExecuted == null)
            return null;
        return Skills[skillExecuted].gameObject.GetComponent<SkillEffects>();
    }

    public static void ApplyTimeBetweenSkillsCooldown()
    {
        for (int i = 1; i <= 3; i++)
            Skills["Action key " + i].ApplyTBSCooldown();
    }

    public static void ReduceCooldowns(float sec)
    {
        for (int i = 1; i <= 3; i++)
            Skills["Action key " + i].cooldown -= sec;
    }
}
