using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_Skill_Info : MonoBehaviour
{
    [SerializeField] string skillName;
    [SerializeField] float cooldown;
    [SerializeField] string description;
    [SerializeField] float skillTime;

    private float cooldownTimer = 0.0f;
    private float lifetimeTimer = 0.0f;
    public string fileName;

    private GameObject skillObject;
    public string keyBinding;

    private Animator characterAnimator;

    //hover control
    private void OnEnable()
    {
        characterAnimator = GameObject.Find("Kgirls01").GetComponent<Animator>();
        //add skill to Action bar and make it executable
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
        skillObject = GameObject.Find("Kgirls01").transform.Find(fileName + "(Clone)").gameObject;
        skillObject.SetActive(false);
        UI_skillsManage.Skills[keyBinding] = this;
        UI_skillsManage.SkillsTemp[keyBinding] = this;
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
        {
            transform.GetChild(0).GetComponent<Image>().fillAmount = ((cooldownTimer - Time.time) * 1 / cooldown);
            return;
        }
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
        CharacterMovement.isImmobilized = false;
        characterAnimator.SetBool("Hit", false);
        lifetimeTimer = 0;
        skillObject.SetActive(false);
    }
}
