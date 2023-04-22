using UnityEngine;

public class UpdateCounts : MonoBehaviour
{
    public static bool update = false;
    [SerializeField] string slotName;
    private void OnEnable()
    {
        if (!update)
            return;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform herbSlot = transform.GetChild(i).Find(slotName);
            herbSlot.GetComponent<DisplayHerb>().UpdateCount();
        }
        update = false;
    }
}
