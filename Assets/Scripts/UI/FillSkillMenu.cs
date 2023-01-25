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
            GameObject skill = (GameObject)Instantiate(Resources.Load("Skill_prefabs/UI/" + f.Name.Remove(f.Name.Length - 7)) /*Remove "Clone"*/, skillSlot);
            skill.transform.SetSiblingIndex(1);
            skill.GetComponent<RectTransform>().anchoredPosition = new Vector3(4.5635e-07f, -4.5635e-07f, 0f); //skill slot position
            skill.GetComponent<RectTransform>().sizeDelta = new Vector2(0.41084f, 0.41084f);
            skillSlot.Find("SkillName").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().skillName;
            skillSlot.Find("SkillDescription").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().description;
            skillSlot.Find("CDnumber").GetComponent<Text>().text = skill.GetComponent<UI_Skill_Execution>().cooldown.ToString() + "s";
            childNum++;
        }
    }

    
}
