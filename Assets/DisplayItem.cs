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

    public void Display(Item item = null)
    {
        Image image = GetComponent<Image>();
        image.sprite = (item == null) ? this.item.icon : item.icon;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 1f);
        if (item.isOwned)
            mask.SetActive(false);
    }

    public void Remove()
    {
        Image image = GetComponent<Image>();
        image.sprite = null;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 0f);
        item = null;
        mask.SetActive(true);
    }

    public Item GetItem()
    {
        return item;
    }
}
