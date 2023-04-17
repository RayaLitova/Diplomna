using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Usables_UI_manager : UI_manager
{
    public Dictionary<string, KeyCode> keyCodes;
    public float timeBetweenExecutes = 2f;

    private string executed = null;
    private string lastExecuted = null;

    private void Start()
    {
        keyCodes = new Dictionary<string, KeyCode>()
        {
            { "Slot 1", KeyCode.Alpha1 },
            { "Slot 2", KeyCode.Alpha2},
            { "Slot 3", KeyCode.Alpha3},
        };
    }
    private void Update()
    {
        for (int i = 0; i < 3; i++) // cooldown
        {
            try
            {
                Executable tmp = ((Executable)slotDisplay["Slot " + (i+1)].Get());
                slotTransform[i].Find("Cooldown").GetComponent<Image>().fillAmount = (tmp.cooldownTimer - Time.time) * 1 / tmp.cooldown;
            }
            catch (Exception) { };
        }

        for(int i = 1; i < 4; i++)
        {
            if(Input.GetKeyDown(keyCodes["Slot " + i]))
            {
                try
                {
                    executed = "Slot " + i;
                    lastExecuted = executed;
                    ((Executable)objects["Slot " + i]).Execute();
                    ApplyTimeBetweenSkillsCooldown();
                }
                catch (NullReferenceException) { };
            }
        }
    }

    public void ReduceCooldowns(float sec)
    {
        for (int i = 1; i <= 3; i++)
        {
            try
            {
                ((Executable)objects["Slot " + i]).ReduceCooldown(sec);
            }
            catch (Exception) { };
        }
    }

    public void ApplyTimeBetweenSkillsCooldown()
    {
        for (int i = 1; i <= 3; i++)
        {
            try
            {
                ((Executable)objects["Slot " + i]).ApplyTBSCooldown();
            }
            catch (Exception)
            {
                continue;
            }
        }
    }

}
