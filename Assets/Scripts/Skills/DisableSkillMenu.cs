using UnityEngine;

public class DisableSkillMenu : MonoBehaviour
{
    private void Update()
    {
        if (GetCurrentRoom.CheckRooms(transform) == null)
            return;

        Destroy(GameObject.Find("SkillMenu"));

        //Disable skills position change
        Transform skills = GameObject.Find("Skills").transform; 
        for (int i = 0; i < skills.childCount; i++)
            Destroy(skills.GetChild(i).GetComponent<SkillPointerEvents>());

        Destroy(this);
    }
}
