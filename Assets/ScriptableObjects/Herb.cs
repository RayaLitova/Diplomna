using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Herb", menuName = "Herb")]
public class Herb : Usable
{

    public static Dictionary<HerbColor, Color> UIcolors = new Dictionary<HerbColor, Color>()
    {
        { HerbColor.Blue, new Color(0, 0, 255)},
        { HerbColor.Red, new Color(255, 0, 0)},
        { HerbColor.Yellow, new Color(255, 191, 0)},
        { HerbColor.Green, new Color(24, 161, 0)},
        { HerbColor.Gray, new Color(140, 140, 140)},
    };
    public enum HerbColor
    {
        Blue,
        Green,
        Gray,
        White,
        Yellow,
        Red,
        Length
    }

    public HerbColor color;
}
