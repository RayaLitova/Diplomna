using System;
using UnityEngine;

public class SkillParticlesInstantiate : MonoBehaviour
{
    public string parentName = "Player";
    [SerializeField] string skillName;
    [NonSerialized] public string fileName;
    private void OnEnable()
    {
        fileName = StaticFunctions.RemoveWhitespace(skillName);
        Instantiate(Resources.Load<GameObject>("Skill_prefabs/Skills/" + fileName), GameObject.Find(parentName).transform.position, Quaternion.identity, GameObject.Find(parentName).transform);
    }

}
