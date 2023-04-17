using UnityEngine;

public class FillInventory : FillBag
{
    public static Transform inventory;
    public static int itemsToUnlock = 0;
    void Start()
    {
        Fill();
        inventory = transform;
        SavingManager.gameData.Items.Remove(folder);
    }

    private void OnEnable()
    {
        if (itemsToUnlock == 0)
            return;

        Transform inventory = FillInventory.inventory;
        DisplayItem item;
        for (int i = 0; i < itemsToUnlock; i++)
        {
            item = inventory.GetChild(Random.Range(0, UnlockItem.itemCount)).GetChild(1).GetComponent<DisplayItem>();

            while (item.Get() != null && ((Item)item.Get()).isOwned) //make sure the item is not already acquired 
                item = inventory.GetChild(Random.Range(0, UnlockItem.itemCount)).GetChild(1).GetComponent<DisplayItem>();

            item.AcquireItem();
        }
        itemsToUnlock = 0;
    }
}
