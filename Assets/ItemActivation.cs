using UnityEngine;

public class ItemActivation : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().AddItem(GetComponent<DisplayItem>().GetItem());
        transform.parent.Find("Mask").gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().RemoveItem(GetComponent<DisplayItem>().GetItem());
        transform.parent.Find("Mask").gameObject.SetActive(true);
    }
}
