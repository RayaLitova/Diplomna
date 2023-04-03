using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill_Execution : MonoBehaviour
{
    private float cooldownTimer = 0.0f;
    public string skillName;
    public float cooldown;
    public string description;
    public enum SkillTypes { damage, buff }
    [SerializeField] SkillTypes skillType;

    private CharacterAnimationController characterAnimator;
    private Transform parentGameObject;
    [NonSerialized] public string fileName;
    public bool isCDRapplied = false; //cooldown reduction
    private UI_Buff_additional buff;

    private void Start()
    {
        fileName = GetComponent<SkillParticlesInstantiate>().fileName;
        characterAnimator = GameObject.Find("Player").GetComponent<CharacterAnimationController>();
        parentGameObject = GameObject.Find(GetComponent<SkillParticlesInstantiate>().parentName).transform;
        parentGameObject.Find(fileName).gameObject.SetActive(false);
        Destroy(GetComponent<SkillParticlesInstantiate>()); // prevents second particles being instantiated
        buff = GetComponent<UI_Buff_additional>();
    }
    private void Update()
    {
        transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown); //UI cooldown
    }

    public void StartExecution()
    {
        if (cooldownTimer > Time.time)
            return;

        GameObject skillObject = parentGameObject.Find(fileName).gameObject;
        skillObject.SetActive(true); // activate skill (Player child object)
        cooldownTimer = Time.time + cooldown;
        

        float index = (1.0f / UI_skillsManage.SkillAnimationCount) * UI_skillsManage.SkillAnimationIndex[fileName];
        index += index == 0f ? 0f : (1.0f / UI_skillsManage.SkillAnimationCount) / 2; //make sure the right animation is playing

        characterAnimator.SetHit(true, index);
        UI_skillsManage.ApplyTimeBetweenSkillsCooldown();
        StartCoroutine("StartSkillExecution");
    }

    private IEnumerator StartSkillExecution()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject skillObject = parentGameObject.Find(fileName).gameObject;

        skillObject.GetComponent<SkillExecution>().ExecuteSkill();

    }

    public void FinishExecution(bool hasBeenExcuted = false)
    {
        characterAnimator.SetHit(false);
        parentGameObject.Find(fileName).gameObject.SetActive(false);

        if (hasBeenExcuted) //because of UI_skillsManage.FinishSkillExecution()
        {
            if (skillType == SkillTypes.damage)
                GameObject.Find("Player").GetComponent<CharacterStats>().ResetBuffs();
            if (buff != null)
                buff.ExecuteBuff();
        }
    }

    public void DestroySkill()
    {
        Destroy(parentGameObject.Find(fileName).gameObject);
    }

    public void ApplyTBSCooldown() // time between skills
    {
        if (cooldownTimer < UI_skillsManage.timeBetweenSkills + Time.time)
            cooldownTimer = Time.time + UI_skillsManage.timeBetweenSkills;
    }
}
