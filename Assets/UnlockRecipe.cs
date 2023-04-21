using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRecipe : MonoBehaviour
{
    private void OnDestroy()
    {
        if(Random.Range(0, 100) < 20)
        {
            foreach (var e in Resources.LoadAll<Food>("Tea/"))
            {
                if (e.isRecipeKnown)
                    continue;

                e.isRecipeKnown = true;
                Debug.Log("Unlocked Recipe");
                break;
            }

        }
    }
}
