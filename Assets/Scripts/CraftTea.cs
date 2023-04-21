using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftTea : MonoBehaviour
{
    public static Dictionary<Herb.HerbColor, int> herbColorCounts = new Dictionary<Herb.HerbColor, int>();

    [SerializeField] private Transform slotsParent;
    [SerializeField] private DisplayFood resultSlot;

    public static void FillColors()
    {
        if (herbColorCounts.Keys.Count != 0)
            return;

        foreach (var e in Resources.LoadAll<Herb>("HerbItems/"))
        {
            if (herbColorCounts.ContainsKey(e.color))
                herbColorCounts[e.color]++;
            else
                herbColorCounts.Add(e.color, 1);
        }
    }

    public void Generate()
    {
        Herb[] recipe = new Herb[3];
        for (int i = 0; i < slotsParent.childCount; i++)
            recipe[i] = (Herb)slotsParent.GetChild(i).Find("Herb").GetComponent<DisplayHerb>().Get();

        if (recipe.Contains(null))
            return; //need 3 herbs 

        foreach (var e in Resources.LoadAll<Food>("Tea/"))
        {
            if (StaticFunctions.CheckForMatch(e.recipe, recipe, 3))
            {
                Debug.Log(e.name);
                resultSlot.DisplayObj(e);
                e.UnlockRecipe();
                return; //success
            } 
        }

        return; // no recipe found

        
    }
    
}
