using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDisplay : MonoBehaviour
{
    public Display Get()
    {
        return transform.Find("ConstSlot").Find("Item").GetComponent<Display>();
    }

    public Food GetItem()
    {
        return (Food)Get().Get();
    }
}
