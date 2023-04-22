using UnityEngine.UI;
using System;

public class DisplayFood : Display
{
    public override void DisplayObj(Usable objRef = null)
    {
        base.DisplayObj(objRef);
        GetComponent<Image>().color = Food.UIcolors[((Food)obj).color];
        UpdateCount();
    }

    public void UpdateCount()
    {
        if (obj == null)
            return;
        try // for const slots
        {
            transform.parent.Find("Count").GetComponent<Text>().text = SavingManager.gameData.Items["Tea"][obj.name].ToString();
        }
        catch (Exception) { };
    }
}
