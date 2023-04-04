using UnityEngine;
using UnityEngine.UI;
public class FillSkillMenu : MonoBehaviour
{
    void Start()
    {
        int childNum = 0;
        foreach (GameObject skill in Resources.LoadAll<GameObject>("Skill_prefabs/UI"))
        {
            Transform skillSlot = transform.GetChild(childNum);
            GameObject newSkill = Instantiate(skill, skillSlot);
            newSkill.transform.SetSiblingIndex(1);
            skillSlot.Find("SkillName").GetComponent<Text>().text = newSkill.GetComponent<UI_Skill_Execution>().skillName;
            skillSlot.Find("SkillDescription").GetComponent<Text>().text = newSkill.GetComponent<UI_Skill_Execution>().description;
            skillSlot.Find("CDnumber").GetComponent<Text>().text = newSkill.GetComponent<UI_Skill_Execution>().cooldown.ToString() + "s";
            childNum++;
        }
    } 
}
