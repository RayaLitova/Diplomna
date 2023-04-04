using UnityEngine;

public class SetUnlockItemStatics : MonoBehaviour
{
    [SerializeField] private GameObject unlockItemParticles;

    void Start()
    {
        UnlockItem.itemCount = Resources.LoadAll<Item>("Items").Length;
        UnlockItem.unlockParticles = unlockItemParticles;
    }
}
