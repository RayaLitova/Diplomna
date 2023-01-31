using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    [SerializeField] private int chance = 50;
    private void OnDestroy()
    {
        if (Random.Range(0, 100) > chance)
            return;
        Debug.Log("unlock");
        Transform inventory = GameObject.Find("Inventory").transform.GetChild(0).GetChild(0);
        inventory.GetChild(1/*Random.Range(0, inventory.childCount)*/).GetChild(1).GetComponent<DisplayItem>().AcquireItem();
    }
}
