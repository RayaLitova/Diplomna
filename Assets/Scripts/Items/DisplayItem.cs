using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : Display
{
    [SerializeField] private GameObject mask;

    private void Start()
    {
        if (obj != null)
        {
            DisplayObj();
        }
    }

    public override void DisplayObj(Usable objRef = null)
    {
        if (GetComponent<ItemActivation>().enabled == true) // in case of putting an item on top of another item
            Remove();

        base.DisplayObj(objRef);

        if (((Item)obj).isOwned || LoadScene.GetCurrentSceneName() == "TargetDummyRoom")
            mask.SetActive(false);
    }

    public override void Activate(Usable objRef = null, bool playParticles = false)
    {
        base.Activate(objRef);
        GetComponent<ItemActivation>().enabled = true;
        if(playParticles)
            GetComponent<ItemActivation>().PlayParticles();
    }

    public override void Remove()
    {
        GetComponent<ItemActivation>().enabled = false;
        base.Remove();
    }
    public void AcquireItem()
    {
        if (obj == null || ((Item)obj).isOwned)
            return;

        ((Item)obj).isOwned = true;
        DisplayObj();
    }
}
