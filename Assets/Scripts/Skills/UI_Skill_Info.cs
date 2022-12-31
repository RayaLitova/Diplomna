using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_Skill_Info : MonoBehaviour
{
    [SerializeField] string skillName;
    public string fileName;
    public string keyBinding;
    private void OnEnable()
    {
        //add skill to Action bar and make it executable
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
        UI_skillsManage.Skills[keyBinding] = GetComponent<UI_Skill_Execution>();
        UI_skillsManage.SkillsTemp[keyBinding] = GetComponent<UI_Skill_Execution>();
    }

}
