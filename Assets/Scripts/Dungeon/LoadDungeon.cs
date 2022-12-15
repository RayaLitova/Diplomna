using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDungeon : MonoBehaviour
{
    [SerializeField] private int dungeon_count;

    private void Awake()
    {
        StaticFunctions.dungeonLevel++;
        Instantiate((GameObject)Resources.Load("DungeonPrefabs/Dungeon_" + Random.Range(1, dungeon_count + 1), typeof(GameObject)), Vector3.zero, Quaternion.identity);
    }
}
