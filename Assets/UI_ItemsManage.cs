using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemsManage : MonoBehaviour
{
    public static Dictionary<string, ItemActivation> items;
    public static Dictionary<string, ItemActivation> itemsTmp;

    public RectTransform[] itemSlotTransform;
    public Dictionary<string, Vector3> itemSlotAnchoredPosition;

    private void Awake()
    {
        itemSlotAnchoredPosition = new Dictionary<string, Vector3>() {
            { "Slot 1", itemSlotTransform[0].anchoredPosition },
            { "Slot 2", itemSlotTransform[1].anchoredPosition },
            { "Slot 3", itemSlotTransform[2].anchoredPosition },
        };

        items = new Dictionary<string, ItemActivation>()
        {
            {"Slot 1", null},
            {"Slot 2", null},
            {"Slot 3", null},
        };

        itemsTmp = new Dictionary<string, ItemActivation>() //used for moving skills
        {
            {"Slot 1", null},
            {"Slot 2", null},
            {"Slot 3", null},
        };
    }
    public string getClosestItemSlot(Vector3 itemPosition) // for skill position change
    {
        float min = 115.0f;
        string returnValue = null;
        float tmp = Vector3.Distance(itemPosition, itemSlotTransform[0].position);
        if (tmp < min)
        {
            min = tmp;
            returnValue = "Slot 1";
        }
        tmp = Vector3.Distance(itemPosition, itemSlotTransform[1].position);
        if (tmp < min)
        {
            min = tmp;
            returnValue = "Slot 2";
        }
        tmp = Vector3.Distance(itemPosition, itemSlotTransform[2].position);
        if (tmp < min)
            returnValue = "Slot 3";

        return returnValue;
    }

}
