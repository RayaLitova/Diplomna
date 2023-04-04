using UnityEngine;
using System.IO;

public class FillInventory : MonoBehaviour
{
    void Start()
    {
        int childNum = 0;
        foreach (Item item in Resources.LoadAll<Item>("Items"))
        {
            Transform itemSlot = transform.GetChild(childNum).GetChild(1);
            itemSlot.GetComponent<DisplayItem>().Display(item);
            childNum++;
        }
    }
}
