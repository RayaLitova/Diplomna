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

    private GameObject skillPointer;
    //hover control
    private void OnEnable()
    {
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
        skillPointer = GameObject.Find("Kgirls01").transform.Find(fileName + "(Clone)").gameObject;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (lifetimeTimer < Time.time && lifetimeTimer != 0)
        {
            lifetimeTimer = 0;
            gameObject.SetActive(false);
        }

        if (cooldownTimer > Time.time)
        {
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
            return;
        }

        if (Input.GetKeyDown(Skills_UI.keyCodes[keyBinding]))
        {
            cooldownTimer = Time.time + cooldown;
            //mana
            GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", true);
            GameObject.Find("Kgirls01").GetComponent<Animator>().SetFloat("SpellIndex", (1 / StaticFunctions.SkillAnimationCount) * StaticFunctions.SkillAnimationIndex[fileName]);

            gameObject.SetActive(true);
            lifetimeTimer = Time.time + skillTime;
        }
    }
}
