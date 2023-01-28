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

    public void Activate(Item itemRef = null)
    {
        Display(itemRef);
        GetComponent<ItemActivation>().enabled = true;
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
}
