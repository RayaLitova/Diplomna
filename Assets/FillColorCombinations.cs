using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillColorCombinations : MonoBehaviour
{
    /*private void Start()
    {
        CraftTea.usedColorVariations = new Dictionary<Herb.HerbColor[], int>();
        Food food = FindObjectOfType<Food>();
        for (int i = 0; i < (int)Food.FoodColor.Length; i++)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("CraftingUI/ColorCombination"));
            Transform colors = temp.transform.Find("Colors");


            foreach(var e in food.GenerateColorCombination((Food.FoodColor)i))
                colors.GetChild(i).GetComponent<Image>().color = Herb.UIcolors[e];
            colors.GetChild(3).GetComponent<Image>().color = Food.UIcolors[(Food.FoodColor)i];
            
        }
    }*/
}
