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
    [SerializeField] float skillTime;

    private float cooldownTimer = 0.0f;
    private float lifetimeTimer = 0.0f;
    public string fileName;

    private GameObject skillPointer;
    public string keyBinding;
    //hover control
    private void OnEnable()
    {
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
        skillPointer = GameObject.Find("Kgirls01").transform.Find(fileName + "(Clone)").gameObject;
        skillPointer.SetActive(false);
        Skills_UI.Skills[keyBinding] = this;
        Skills_UI.SkillsTemp[keyBinding] = this;
    }

    private void Update()
    {
        if (lifetimeTimer < Time.time && lifetimeTimer != 0)
        {
            GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", false);
            lifetimeTimer = 0;
            FinishExecution();
        }

        if (cooldownTimer > Time.time)
        {
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
            return;
        }
    }

    public void Execute()
    {
        if (cooldownTimer > Time.time) return;
        skillPointer.SetActive(true);
        cooldownTimer = Time.time + cooldown;
        lifetimeTimer = Time.time + skillTime;
        GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", true);
        GameObject.Find("Kgirls01").GetComponent<Animator>().SetFloat("SpellIndex", (1.0f / Skills_UI.SkillAnimationCount) * Skills_UI.SkillAnimationIndex[fileName]);
    }

    public void FinishExecution(bool isEnemyHit = false)
    {
        GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", false);
        lifetimeTimer = 0;
        skillPointer.transform.Find(fileName + "_particles").gameObject.SetActive(true);
        skillPointer.SetActive(false);
    }
}