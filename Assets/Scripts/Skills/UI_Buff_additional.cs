using UnityEngine;

public class UI_Buff_additional : MonoBehaviour
{
    [SerializeField] private int bonusATK;
    [SerializeField] private int bonusDEF;
    [SerializeField] private int bonusCrit;
    [SerializeField] private int bonusAgility;
    [SerializeField] private int healing;

    private CharacterStats characterStats;

    public void ExecuteBuff()
    {
        characterStats.isBuffed = true;
        characterStats.bonusATK += bonusATK;
        characterStats.bonusDEF += bonusDEF;
        characterStats.bonusCrit += bonusCrit;
        characterStats.bonusAgility += bonusAgility;
        characterStats.healing += healing;
    }
}
