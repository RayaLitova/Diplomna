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

        Transform inventory = GameObject.Find("Inventory").transform.GetChild(0).GetChild(0);
        DisplayItem item = inventory.GetChild(Random.Range(0, itemCount)).GetChild(1).GetComponent<DisplayItem>();

        while (item.Get() != null && ((Item)item.Get()).isOwned) //make sure the item is not already acquired 
            item = inventory.GetChild(Random.Range(0, itemCount)).GetChild(1).GetComponent<DisplayItem>();
        
        item.AcquireItem();
    }
}
