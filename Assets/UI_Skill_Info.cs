using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill_Info : MonoBehaviour
{
    [SerializeField] string skillName;
    [SerializeField] int cost;
    [SerializeField] float cooldown;
    [SerializeField] string description;
    [SerializeField] string keyBinding;
    [SerializeField] float skillTime;

    private float cooldownTimer = 0.0f;
    private float lifetimeTimer = 0.0f;
    private string fileName;

    private Dictionary<string, KeyCode> keyCodes;

    //cooldown control
    //hover control

    void Start()
    {
        keyCodes = new Dictionary<string, KeyCode>()
        {
            {"Action key 1", KeyCode.Q },
            {"Action key 2", KeyCode.E },
            {"Action key 3", KeyCode.R },
        };

        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Debug.Log(fileName);
    }

    private void Update()
    {
        if (lifetimeTimer < Time.time && lifetimeTimer != 0)
            Destroy(GameObject.Find(fileName + "(Clone)"));

        if (cooldownTimer > Time.time)
        {
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
            return;
        }

        if (Input.GetKeyDown(keyCodes[keyBinding]))
        {
            cooldownTimer = Time.time + cooldown;
            //mana
            GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", true);
            GameObject.Find("Kgirls01").GetComponent<Animator>().SetFloat("SpellIndex", (1 / StaticFunctions.SkillAnimationCount) * StaticFunctions.SkillAnimationIndex[fileName]);

            Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
            lifetimeTimer = Time.time + skillTime;
        }
    }
}
