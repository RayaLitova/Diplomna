using UnityEngine;

public class FillBag : MonoBehaviour
{
    [SerializeField] protected string folder;
    void Start()
    {
        Fill();
    }

    public void Fill()
    {
        int childNum = 0;
        foreach (Usable obj in Resources.LoadAll<Usable>(folder))
        {
            Transform slot = transform.GetChild(childNum).GetChild(1);
            slot.GetComponent<Display>().DisplayObj(obj);
            childNum++;
        }
    }
}
