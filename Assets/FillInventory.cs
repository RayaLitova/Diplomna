using UnityEngine;
using System.IO;

public class FillInventory : MonoBehaviour
{
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Items");
        FileInfo[] info = dir.GetFiles("*.asset");
        int childNum = 0;
        foreach (FileInfo f in info)
        {
            Transform itemSlot = transform.GetChild(childNum).GetChild(1);
            itemSlot.GetComponent<DisplayItem>().Display(Resources.Load<Item>("Items/" + f.Name.Remove(f.Name.Length - 6)));
            childNum++;
        }
    }
}
