using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Herb")]
public class Herb : Usable
{
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
