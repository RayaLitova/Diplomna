using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Skill_Info : MonoBehaviour
{
    [SerializeField] string skillName;
    [SerializeField] int cost;
    [SerializeField] float cooldown;
    [SerializeField] string description;
    [SerializeField] string keyBinding;

    private float timer = 0.0f;

    private Dictionary<string, KeyCode> keyCodes;

    //cooldown control
    //hover control

    void Start()
    {
        keyCodes = new Dictionary<string, KeyCode>()
        {
            {"Q", KeyCode.Q },
            {"E", KeyCode.E },
            {"R", KeyCode.R },
        };
        
    }

    private void Update()
    {
        if (timer > Time.time) return;

        if (Input.GetKeyDown(keyCodes[keyBinding]))
        {
            timer = Time.time + cooldown;
            //mana
            //apply ui cooldown effect
        }
    }
}
