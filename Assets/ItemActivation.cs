using UnityEngine;

public class ItemActivation : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().AddItem(GetComponent<DisplayItem>().GetItem());
    }
    private void OnDisable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().RemoveItem(GetComponent<DisplayItem>().GetItem());
        GetComponent<DisplayItem>().Remove();
    }
}
