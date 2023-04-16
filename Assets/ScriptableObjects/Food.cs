using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : Usable
{
    public enum FoodType 
    { 
        Tea,
        Potion,
        Food,
        Length
    }
    public FoodType type;

    public int bonusATK = 0;
    public int bonusDEF = 0;
    public int bonusCrit = 0;
    public int bonusAgility = 0;
    public int bonusMaxHealth = 0;
    public int bonusRegen = 0;
    public float cooldownReduction = 0f;
    public int healing = 0;
    public float timeS = 0f;

    public int count = 0;
}
