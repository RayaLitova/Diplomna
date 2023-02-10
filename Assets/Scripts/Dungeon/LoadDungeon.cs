using UnityEngine;

public class LoadDungeon : MonoBehaviour
{
    [SerializeField] private int dungeon_count;
    public static int dungeonLevel = 0;
    public static Vector3 cinematicCameraRotation = Vector3.zero;

    private void Awake()
    {
        if (dungeonLevel == 0)
        {
            Instantiate(Resources.Load<GameObject>("TutorialDungeon/Dungeon"), Vector3.zero, Quaternion.identity);
            cinematicCameraRotation.y = -90; //Rotate camera for portal activation cutscene
            return;
        }
        int dungeonNumber = Random.Range(1, dungeon_count + 1);
        Instantiate(Resources.Load<GameObject>("DungeonPrefabs/Dungeon_" + dungeonNumber), Vector3.zero, Quaternion.identity);
        cinematicCameraRotation.y = dungeonNumber == 2 ? 90 : -90; //Rotate camera for portal activation cutscene
    }
}