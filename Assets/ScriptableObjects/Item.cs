using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public new string description;
    public Sprite icon;

    public int bonusATK;
    public int bonusDEF;
    public int bonusCrit;
    public int bonusAgility;
    public int bonusMaxHealth;
    public int bonusRegen;

    public new string skillUnlocked; //?
    }
