using UnityEngine;
using System.Linq;
using System;

public class SpawnEnemies : MonoBehaviour
{
    public static int enemyCount;
    private bool[] usedPositions = new bool[15];

    private void Awake()
    {
        enemyCount = UnityEngine.Random.Range(1, 5);
        Array.Fill(usedPositions, false);
        for (int i = 0; i < enemyCount; i++)
        {
            if (!usedPositions.Contains(false)) //in case there are more enemies than positions
                return;

            int positionNumber;
            do {
                positionNumber = UnityEngine.Random.Range(0, transform.childCount);
            } while (usedPositions[positionNumber] == true && usedPositions.Contains(false)); //Make sure spawn position is not used
            usedPositions[positionNumber] = true;
            Instantiate((GameObject)Resources.Load<GameObject>("Enemies/Skeleton_" + UnityEngine.Random.Range(0, 4)), transform.GetChild(positionNumber).position, Quaternion.identity);
        }
    }
}
