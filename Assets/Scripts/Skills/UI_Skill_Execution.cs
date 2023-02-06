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
    public bool isCDRapplied = false; //cooldown reduction

    private void Start()
    {
        fileName = GetComponent<SkillParticlesInstantiate>().fileName;
        characterAnimator = GameObject.Find("Player").GetComponent<Animator>();
        skillObject = GameObject.Find(GetComponent<SkillParticlesInstantiate>().parentName).transform.Find(fileName + "(Clone)").gameObject;
        skillObject.SetActive(false);
        Destroy(GetComponent<SkillParticlesInstantiate>()); // prevents second particles being instantiated
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

        float index = (1.0f / UI_skillsManage.SkillAnimationCount) * UI_skillsManage.SkillAnimationIndex[fileName];
        index += index == 0f ? 0f : (1.0f / UI_skillsManage.SkillAnimationCount) / 2; //make sure the right animation is playing

        characterAnimator.SetFloat("SpellIndex", index);
        skillObject.GetComponent<SkillExecution>().ExecuteSkill();
        UI_skillsManage.ApplyTimeBetweenSkillsCooldown();
        try
        {
            GetComponent<UI_Buff_additional>().ExecuteBuff(); //if it's already destroyed
        }
        catch (Exception) { };
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

    public void ApplyTBSCooldown() // time between skills
    {
        if (cooldownTimer < Time.time)
            cooldownTimer = Time.time + UI_skillsManage.timeBetweenSkills;
    }
}
