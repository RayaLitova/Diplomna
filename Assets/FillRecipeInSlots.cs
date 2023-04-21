using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillRecipeInSlots : MonoBehaviour
{
    GameObject herbSlots;
    public Food item;
    void Start()
    {
        herbSlots = GameObject.Find("HerbSlots");
    }

    public void Fill()
    {
        Debug.Log(item);
        for (int i = 0; i < 3; i++)
        {
            herbSlots.transform.GetChild(i).Find("Herb").GetComponent<DisplayHerb>().DisplayObj(item.recipe[i]);
            herbSlots.transform.GetChild(i).Find("Herb").GetComponent<PointerEvents>().moveItem("Slot " + (i+1).ToString());
        }
    }
}
