using UnityEngine;

public class FillBag : MonoBehaviour
{
    [SerializeField] protected string folder;
    void Start()
    {
        Fill();
    }

    public virtual void Fill()
    {
        int childNum = 0;
        foreach (Usable obj in Resources.LoadAll<Usable>(folder))
        {
            AddElement(obj, childNum);
            childNum++;
        }
    }

    public void AddElement(Usable obj, int slotnum)
    {
        Transform slot = transform.GetChild(slotnum).GetChild(1);
        slot.GetComponent<Display>().DisplayObj(obj);
    }
}