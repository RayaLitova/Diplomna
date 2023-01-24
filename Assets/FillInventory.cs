using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FillInventory : MonoBehaviour
{
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/resources/Items");
        FileInfo[] info = dir.GetFiles("*.asset");
        int childNum = 0;
        foreach (FileInfo f in info)
        {
            Transform itemSlot = transform.GetChild(childNum);
            itemSlot.GetComponent<DisplayItem>().item = Resources.Load<Item>("Items/" + f.Name.Remove(f.Name.Length - 6));
            itemSlot.GetComponent<DisplayItem>().Display();
            childNum++;
        }
    }
}
