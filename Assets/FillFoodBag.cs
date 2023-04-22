using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillFoodBag : FillBag
{
    public override void Fill()
    {
        int childNum = 0;
        foreach (Usable obj in Resources.LoadAll<Usable>(folder))
        {
            if (SavingManager.gameData.Items["Tea"][obj.name] <= 0)
                continue;
            AddElement(obj, childNum);
            childNum++;
        }
    }
}
