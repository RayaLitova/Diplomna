using UnityEngine;

public class UI_Skill_Info : MonoBehaviour
{
    [SerializeField] string skillName;
    public string fileName;
    private void OnEnable()
    {
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate((GameObject)Resources.Load("Skill_prefabs/Skills/" + fileName, typeof(GameObject)), GameObject.Find("Kgirls01").transform);
    }

}
