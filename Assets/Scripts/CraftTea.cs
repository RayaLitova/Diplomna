using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTea : MonoBehaviour
{
    public static Dictionary<Herb.HerbColor, int> herbColorCounts = new Dictionary<Herb.HerbColor, int>();
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
}
