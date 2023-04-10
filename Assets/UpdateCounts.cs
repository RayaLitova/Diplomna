using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCounts : MonoBehaviour
{
    public static bool update = false;
    private void OnEnable()
    {
        if (!update)
            return;

        int childNum = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform herbSlot = transform.GetChild(i).GetChild(1);
            herbSlot.GetComponent<DisplayHerb>().UpdateCount();
        }
    }
}
