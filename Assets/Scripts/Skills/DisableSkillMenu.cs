using UnityEngine;

public class DisableSkillMenu : MonoBehaviour
{
    private void Update()
    {
        if (GetCurrentRoom.CheckRooms(transform) == null)
            return;

        Destroy(GameObject.Find("SkillMenu"));
        Destroy(GameObject.Find("FoodBag"));

        //Disable skills position change
        Transform skills = GameObject.Find("Skills").transform; 
        for (int i = 0; i < skills.childCount; i++)
            Destroy(skills.GetChild(i).GetComponent<SkillPointerEvents>());
        
        Transform foods = GameObject.Find("FoodSlots").transform; 
        for (int i = 0; i < foods.childCount; i++)
            Destroy(foods.GetChild(i).GetChild(1).GetComponent<PointerEvents>());

        Destroy(this);
    }
}
