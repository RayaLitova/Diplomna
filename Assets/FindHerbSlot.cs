using UnityEngine;

public class FindHerbSlot : MonoBehaviour
{
    public Transform FindSlot(Herb target)
    {
        for (int i = 0; i < transform.childCount; i++)
        { 
            if(transform.GetChild(i).Find("Herb").GetComponent<DisplayHerb>().CheckHerb(target))
                return transform.GetChild(i);
        }
        return null;
    }
}
