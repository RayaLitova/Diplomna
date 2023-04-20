using UnityEngine.UI;

public class DisplayHerb : Display
{ 
    public override void DisplayObj(Usable objRef = null)
    {
        base.DisplayObj(objRef);
        UpdateCount();
    }

    public void UpdateCount()
    {
        if(obj == null)
            return;

        if (!SavingManager.gameData.Items["HerbItems"].ContainsKey(obj.name))
            SavingManager.gameData.Items["HerbItems"][obj.name] = 0;
        transform.parent.Find("Count").GetComponent<Text>().text = SavingManager.gameData.Items["HerbItems"][obj.name].ToString();
    }

    public bool CheckHerb(Herb h)
    {
        return h == (Herb)obj;
    }
}
