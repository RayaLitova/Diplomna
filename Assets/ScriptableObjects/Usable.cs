using UnityEngine;

[CreateAssetMenu(fileName = "New Usable", menuName = "Usable")]
public class Usable : ScriptableObject
{
    public new string name = "";
    public new string description = "";
    public Sprite icon;
}
