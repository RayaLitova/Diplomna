using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static int enemyCount = LoadDungeon.dungeonLevel * 2 + 5;
    private int[] usedPositions = new int[15];

    private void Awake()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int positionNumber;
            do {
                positionNumber = Random.Range(0, transform.childCount);
            } while (usedPositions[positionNumber] == 1); //Make sure spawn position is not used
            usedPositions[positionNumber] = 1;
            Instantiate((GameObject)Resources.Load<GameObject>("Enemies/Skeleton_" + Random.Range(0, 4)), transform.GetChild(positionNumber).position, Quaternion.identity);
        }
    }
}
