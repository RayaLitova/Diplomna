using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    [SerializeField] private int chance = 50;
    public static GameObject unlockParticles;
    public static int itemCount = 0;
    private void OnDestroy()
    {
        if (Random.Range(0, 100) > chance)
            return;

        unlockParticles.SetActive(true);
        FillInventory.itemsToUnlock++;
    }
}
