using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Herb")]
public class Herb : ScriptableObject
{
    public new string name;
    public Sprite icon;

    public int count = 0;
}
