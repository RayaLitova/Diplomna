using UnityEngine;

public class LoadDungeon : MonoBehaviour
{
    [SerializeField] private int dungeon_count;
    public static int dungeonLevel = 0;

    private void Awake()
    {
        if (dungeonLevel == 0)
        {
            Instantiate(Resources.Load<GameObject>("TutorialDungeon/Dungeon"), Vector3.zero, Quaternion.identity);
            return;
        }
        int dungeonNumber = Random.Range(1, dungeon_count + 1);
        Instantiate(Resources.Load<GameObject>("DungeonPrefabs/Dungeon_" + dungeonNumber), Vector3.zero, Quaternion.identity);
    }
}