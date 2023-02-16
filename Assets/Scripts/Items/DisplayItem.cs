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
        if (item != null)
            Remove();

        if (itemRef != null)
            item = itemRef;
        
        Image image = GetComponent<Image>();
        image.sprite = item.icon;
        transform.rotation = new Quaternion(0, 0, 1, 0); //z = 180
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
        transform.parent.parent.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void AcquireItem()
    {
        if (item == null || item.isOwned)
            return;

        item.isOwned = true;
        Display();
    }
}
