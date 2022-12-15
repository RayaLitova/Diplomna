using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] int enemyCount;

    private int[] usedPositions = new int[15];

    private void Awake()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int positionNumber;
            do {
                positionNumber = Random.Range(0, transform.childCount);
            } while (usedPositions[positionNumber] == 1);
            usedPositions[positionNumber] = 1;
            Instantiate((GameObject)Resources.Load("Enemies/Skeleton_" + Random.Range(0, 4), typeof(GameObject)), transform.GetChild(positionNumber).position, Quaternion.identity);
        }
    }
}
