using UnityEngine;

public class FillHerbBag : MonoBehaviour
{
    void Start()
    {
        int childNum = 0;
        foreach (Herb herb in Resources.LoadAll<Herb>("HerbItems"))
        {
            Transform herbSlot = transform.GetChild(childNum).GetChild(1);
            herbSlot.GetComponent<DisplayHerb>().DisplayObj(herb);
            childNum++;
        }
    }
}
