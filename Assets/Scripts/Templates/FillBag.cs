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
        bool isNewGame = false;
        if (!SavingManager.gameData.Items.ContainsKey(folder))
        {
            isNewGame = true;
            SavingManager.gameData.Items[folder] = new();
        }
        int childNum = 0;
        foreach (Usable obj in Resources.LoadAll<Usable>(folder))
        {
            if (isNewGame)
                SavingManager.gameData.Items[folder][obj.name] = 0;
            Transform slot = transform.GetChild(childNum).GetChild(1);
            slot.GetComponent<Display>().DisplayObj(obj);
            childNum++;
        }
    }
}
