using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FillSkillMenu : MonoBehaviour
{
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/StreamingAssets/UI");
        FileInfo[] info = dir.GetFiles("*.prefab");
        int childNum = 0;
        foreach (FileInfo f in info)
        {
            Transform skillSlot = transform.GetChild(childNum);
            GameObject skill = (GameObject)Instantiate(Resources.Load("Skill_prefabs/UI/" + f.Name.Remove(f.Name.Length - 7)) 
                /*Remove ".prefab"*/, skillSlot);
            skill.transform.SetSiblingIndex(1);
            skillSlot.Find("SkillName").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().skillName;
            skillSlot.Find("SkillDescription").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().description;
            skillSlot.Find("CDnumber").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().cooldown.ToString() + "s";
            childNum++;
        }
    }

    
}
