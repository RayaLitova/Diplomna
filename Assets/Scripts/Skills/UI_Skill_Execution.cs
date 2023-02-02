using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill_Execution : MonoBehaviour
{
    private float cooldownTimer = 0.0f;
    public string skillName;
    public float cooldown;
    public string description;

    [Tooltip("damage / buff")] // shows message in the inspector
    [SerializeField] string skillType;

    private Animator characterAnimator;
    private GameObject skillObject;
    [NonSerialized] public string fileName;

    private void Start()
    {
        fileName = GetComponent<UI_Skill_Info>().fileName;
        characterAnimator = GameObject.Find("Player").GetComponent<Animator>();
        skillObject = GameObject.Find(GetComponent<UI_Skill_Info>().parentName).transform.Find(fileName + "(Clone)").gameObject;
        skillObject.SetActive(false);
    }
    private void Update()
    { 
        if (cooldownTimer > Time.time) // UI cooldown control
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
        
    }

    public void StartExecution()
    {
        if (cooldownTimer > Time.time)
            return;
        CharacterMovement.isImmobilized = true; // immobilized while executing skill
        skillObject.SetActive(true); // activate skill (Player child object)
        cooldownTimer = Time.time + cooldown;
        characterAnimator.SetBool("Hit", true);
        characterAnimator.SetFloat("SpellIndex", (1.0f / UI_skillsManage.SkillAnimationCount) * UI_skillsManage.SkillAnimationIndex[fileName]);
        skillObject.GetComponent<SkillExecution>().ExecuteSkill();
    }

    public void FinishExecution()
    {
        if (skillType == "damage")
            GameObject.Find("Player").GetComponent<CharacterStats>().ResetBuffs();
        CharacterMovement.isImmobilized = false;
        characterAnimator.SetBool("Hit", false);
        skillObject.SetActive(false);
    }

    public void DestroySkill()
    {
        Destroy(skillObject);
    }
}
