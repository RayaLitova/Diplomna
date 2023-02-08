using UnityEngine;
using System.IO;

public class SetUnlockItemStatics : MonoBehaviour
{
    [SerializeField] private GameObject unlockItemParticles;

    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Items"); //get all items
        FileInfo[] info = dir.GetFiles("*.asset");

        UnlockItem.itemCount = info.Length;
        UnlockItem.unlockParticles = unlockItemParticles;
    }
}
