using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CraftTea : MonoBehaviour
{
    public static Dictionary<Herb.HerbColor, int> herbColorCounts = new Dictionary<Herb.HerbColor, int>();
    public static Dictionary<Herb.HerbColor[], int> colorVariations = new Dictionary<Herb.HerbColor[], int>();
    public static Dictionary<Herb.HerbColor[], int> usedColorVariations = new Dictionary<Herb.HerbColor[], int>();

    [SerializeField] private Transform slotsParent;
    [SerializeField] private DisplayFood resultSlot;
    [SerializeField] private GameObject herbBag;
    [SerializeField] private GameObject foodBag;

    public static bool CombinationAvailable(Herb.HerbColor[] comb)
    { 
        var key = StaticFunctions.GetMatch(colorVariations.Keys, comb, 3);
        if (key == null)
        {
            colorVariations.Add(comb, CalcVariations(comb));
            usedColorVariations.Add(comb, 0);
            if(colorVariations[comb] == 0)
                return false;
            return true;
        }
        if (usedColorVariations[key] < colorVariations[key])
        {
            return true;
        }
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
        {
            recipe[i] = (Herb)slotsParent.GetChild(i).Find("Herb").GetComponent<DisplayHerb>().Get();
            if (SavingManager.gameData.Items["HerbItems"][recipe[i].name] <= 0)
                return;
        }

        if (recipe.Contains(null))
            return; //need 3 herbs 

        foreach (var e in Resources.LoadAll<Food>("Tea/"))
        {
            if (StaticFunctions.CheckForMatch(e.recipe, recipe, 3))
            {
                for (int i = 0; i < 3; i++)
                {
                    SavingManager.gameData.Items["HerbItems"][recipe[i].name]--;
                    if (herbBag.gameObject.activeInHierarchy)
                        herbBag.GetComponent<FindHerbSlot>().FindSlot(recipe[i]).Find("Count").GetComponent<Text>().text = SavingManager.gameData.Items["HerbItems"][recipe[i].name].ToString();
                }
                SavingManager.gameData.Items["Tea"][e.name]++;

                if (foodBag.gameObject.activeInHierarchy)
                    foodBag.GetComponent<FillFoodBag>().Fill();
                UpdateCounts.update = true;
                resultSlot.DisplayObj(e);
                e.UnlockRecipe();
                return; //success
            } 
        }

        return; // no recipe found

        
    }
    
}
