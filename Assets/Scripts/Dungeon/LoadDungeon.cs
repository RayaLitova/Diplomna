using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDungeon : MonoBehaviour
{
    [SerializeField] private int dungeon_count;
    public static int dungeonLevel = 1;
    public static Vector3 cinematicCameraRotation = Vector3.zero;

    private void Awake()
    {
        int dungeonNumber = Random.Range(1, dungeon_count + 1);
        Instantiate((GameObject)Resources.Load("DungeonPrefabs/Dungeon_" + dungeonNumber, typeof(GameObject)), Vector3.zero, Quaternion.identity);
        cinematicCameraRotation.y = dungeonNumber == 2 ? 90 : -90;
    }
}