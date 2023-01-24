using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    public Item item;

    public void Display()
    {
        Image image = transform.parent.Find("Item").GetComponent<Image>();
        image.sprite = item.icon;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 1f);
    }
}
