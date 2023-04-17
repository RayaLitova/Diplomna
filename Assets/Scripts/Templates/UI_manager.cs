using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_manager : MonoBehaviour
{
    public Dictionary<string, Usable> objects;
    public Dictionary<string, Display> slotDisplay;

    public RectTransform[] slotTransform;
    public Dictionary<string, Vector3> slotAnchoredPosition;

    [SerializeField] string displayItemGoName;

    private void Awake()
    {
        slotTransform = new RectTransform[] {
            transform.GetChild(0).GetComponent<RectTransform>(),
            transform.GetChild(1).GetComponent<RectTransform>(),
            transform.GetChild(2).GetComponent<RectTransform>()
        };
        slotAnchoredPosition = new Dictionary<string, Vector3>() {
            { "Slot 1", slotTransform[0].anchoredPosition },
            { "Slot 2", slotTransform[1].anchoredPosition },
            { "Slot 3", slotTransform[2].anchoredPosition },
        };

        slotDisplay = new Dictionary<string, Display>()
        {
            { "Slot 1", slotTransform[0].Find(displayItemGoName).gameObject.GetComponent<Display>() },
            { "Slot 2", slotTransform[1].Find(displayItemGoName).gameObject.GetComponent<Display>() },
            { "Slot 3", slotTransform[2].Find(displayItemGoName).gameObject.GetComponent<Display>() },
        };

        objects = new Dictionary<string, Usable>()
        {
            {"Slot 1", null},
            {"Slot 2", null},
            {"Slot 3", null},
        };
    }
    public string getClosestSlot(Vector3 pos) 
    {
        float min = 215.0f;
        string returnValue = null;
        float tmp;

        for (int i = 0; i < 3; i++)
        {
            tmp = Vector3.Distance(pos, slotTransform[i].position);
            if (tmp < min)
            {
                min = tmp;
                returnValue = "Slot " + (i + 1).ToString();
            }
            Debug.LogError(tmp);
        }
        Debug.LogError(returnValue);
        return returnValue;
    }
}
