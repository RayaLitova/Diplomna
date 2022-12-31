using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill_Execution : MonoBehaviour
{
    private float cooldownTimer = 0.0f;
    private float lifetimeTimer = 0.0f;
    public string skillName;
    public float cooldown;
    public string description;
    [SerializeField] float skillTime;

    [Tooltip("damage / buff")] // shows message in the inspector
    [SerializeField] string skillType;

    private Animator characterAnimator;
    private GameObject skillObject;
    public string fileName;

    private void Start()
    {
        fileName = GetComponent<UI_Skill_Info>().fileName;
        characterAnimator = GameObject.Find("Kgirls01").GetComponent<Animator>();
        skillObject = GameObject.Find("Kgirls01").transform.Find(fileName + "(Clone)").gameObject;
        skillObject.SetActive(false);
    }
    private void Update()
    {
        if (lifetimeTimer < Time.time && lifetimeTimer != 0) // skill lifetime after execution
        {
            characterAnimator.SetBool("Hit", false);
            lifetimeTimer = 0;
            FinishExecution();
        }

        if (cooldownTimer > Time.time) // UI cooldown control
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
        
    }

    public void StartExecution()
    {
        if (cooldownTimer > Time.time)
            return;
        CharacterMovement.isImmobilized = true; // immobilized while executing skill
        skillObject.SetActive(true); // activate skill (Kgirls01 child object)
        cooldownTimer = Time.time + cooldown;
        lifetimeTimer = Time.time + skillTime;
        characterAnimator.SetBool("Hit", true);
        characterAnimator.SetFloat("SpellIndex", (1.0f / UI_skillsManage.SkillAnimationCount) * UI_skillsManage.SkillAnimationIndex[fileName]);
        skillObject.GetComponent<SkillExecution>().ExecuteSkill();
    }

    public void FinishExecution()
    {
        if (skillType == "damage")
            GameObject.Find("Kgirls01").GetComponent<CharacterStats>().ResetBuffs();
        CharacterMovement.isImmobilized = false;
        characterAnimator.SetBool("Hit", false);
        lifetimeTimer = 0;
        skillObject.SetActive(false);
    }
}
