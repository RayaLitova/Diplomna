using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftTea : MonoBehaviour
{
    public static Dictionary<Herb.HerbColor, int> herbColorCounts = new Dictionary<Herb.HerbColor, int>();
    public static Dictionary<Herb.HerbColor[], int> colorVariations = new Dictionary<Herb.HerbColor[], int>();
    public static Dictionary<Herb.HerbColor[], int> usedColorVariations = new Dictionary<Herb.HerbColor[], int>();

    [SerializeField] private Transform slotsParent;
    [SerializeField] private DisplayFood resultSlot;

    public static bool CombinationAvailable(Herb.HerbColor[] comb)
    { 
        var key = StaticFunctions.GetMatch(colorVariations.Keys, comb, 3);
        Debug.Log(comb[0] + " " + comb[1] + " " + comb[2]);
        if (key == null)
        {
            Debug.Log("NULL");
            colorVariations.Add(comb, CalcVariations(comb));
            usedColorVariations.Add(comb, 0);
            if(colorVariations[comb] == 0)
                return false;
            return true;
        }
        Debug.Log(key[0] + " " + key[1] + " " + key[2]);
        if (usedColorVariations[key] < colorVariations[key])
        {
            Debug.Log("Available");
            return true;
        }
        Debug.Log("NOT AVAILABLE");
        return false;
    }

    public static void IncrementUsedCombinations(Herb.HerbColor[] comb)
    {
        var key = StaticFunctions.GetMatch(colorVariations.Keys, comb, 3);
        usedColorVariations[key]++;
    }

    public static int CalcVariations(Herb.HerbColor[] color)
    {
        int result = 1;
        Dictionary<Herb.HerbColor, int> temp = new Dictionary<Herb.HerbColor, int>(herbColorCounts);
        foreach (var e in color)
        {
            result *= temp[e];
            temp[e]--;
        }
        return result;
    }

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
