using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    private Item item = null;
    [SerializeField] private GameObject mask;

    private void Start()
    {
        if (item != null)
        {
            Display();
        }
    }

    public void Display(Item itemRef = null)
    {
        if (itemRef != null)
            item = itemRef;
        
        Image image = GetComponent<Image>();
        image.sprite = item.icon;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 1f);
        if (item.isOwned)
            mask.SetActive(false);
    }

    public void Activate(Item itemRef = null, bool playParticles = true)
    {
        Display(itemRef);
        GetComponent<ItemActivation>().enabled = true;
        if(playParticles)
            GetComponent<ItemActivation>().PlayParticles();
    }

    public void Remove()
    {
        GetComponent<ItemActivation>().enabled = false;
        Image image = GetComponent<Image>();
        image.sprite = null;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 0f);
        item = null;
    }

    public Item GetItem()
    {
        return item;
    }

    public void ShowItemDescription()
    {
        if (item == null)
            return;

        Transform itemDesc = transform.parent.parent.parent.GetChild(1); // Item description game object
        itemDesc.gameObject.SetActive(true);
        itemDesc.GetChild(1).GetComponent<Text>().text = item.name; // name
        itemDesc.GetChild(2).GetComponent<Text>().text = item.description; // description
    }

    public void HideItemDescription()
    {
        GameObject.Find("ItemDescription").gameObject.SetActive(false);
    }
}
