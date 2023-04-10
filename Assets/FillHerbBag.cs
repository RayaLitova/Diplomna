using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillHerbBag : MonoBehaviour
{
    void Start()
    {
        int childNum = 0;
        foreach (Herb herb in Resources.LoadAll<Herb>("HerbItems"))
        {
            Transform herbSlot = transform.GetChild(childNum).GetChild(1);
            herbSlot.GetComponent<DisplayHerb>().Display(herb);
            childNum++;
        }
    }
}
