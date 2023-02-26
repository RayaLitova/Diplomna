using UnityEngine;
using System.Collections.Generic;
public class GenerateDungeon : MonoBehaviour
{
    private int roomCount;
    private int currentRooms = 1;
    private bool isBossRoomGenerated = false;
    private bool isPortalRoomGenerated = false;
    [SerializeField] GameObject cinematicCamera;

    private enum RoomType
    {
        None = 0,
        SafeRoom,
        NormalRoom,
        BossRoom,
        PortalRoom,
        Length
        
    }

    private Dictionary<RoomType, string> RoomResourcesPath;

    private RoomType[,] grid;

    private int minPathLength;
    private int currPathLength = 0;
    private bool[,] visitedRooms;
    private List<GameObject> currCameraPoints = new List<GameObject>();
    public static List<GameObject> cameraPoints = new List<GameObject>();
    private GameObject[,] allCameraPoints;
    void Start()
    {
        RoomResourcesPath = new Dictionary<RoomType, string>() {
            { RoomType.SafeRoom, "DungeonRooms/SafeRoom" },
            { RoomType.NormalRoom, "DungeonRooms/NormalRoom" },
            { RoomType.BossRoom, "DungeonRooms/BossRoom" },
            { RoomType.PortalRoom, "DungeonRooms/PortalRoom" }
        };


        roomCount = 6 + LoadDungeon.dungeonLevel * 2;
        minPathLength = roomCount; //biggest possible length
        grid = new RoomType[7,7];

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                grid[i, j] = RoomType.None;
            
        
        grid[4,4] = RoomType.SafeRoom;

        visitedRooms = new bool[7, 7];

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                visitedRooms[i, j] = false;

        allCameraPoints = new GameObject[7, 7];

        AddRooms(4, 4);
        DisplayDungeon();
        GenerateStartingCutscenePath(4,4);
        Debug.Log(cameraPoints[0]); //doesnt work
        cinematicCamera.GetComponent<MoveTowardsBoss>().enabled = true;
    }

    private void AddRooms(int a, int b)
    {
        int availableRoomsCount = 0;
        for (int i = -1; i <= 1; i += 2)
        {
            if (a + i >= 0 && a + i < 7 && grid[a + i, b] == RoomType.None)
                availableRoomsCount++;
        }
        for (int j = -1; j <= 1; j += 2)
        {
            if (b + j >= 0 && b + j < 7 && grid[a, b + j] == RoomType.None)
                availableRoomsCount++;
        }

        if (availableRoomsCount == 0 || roomCount - currentRooms == 0)
            return;

        int roomsCount = Random.Range(1, Mathf.Min(availableRoomsCount, roomCount - currentRooms));

        currentRooms += roomsCount;
        for (int i = 0; i < roomsCount; i++) 
        {
            RoomType roomType = (RoomType)Random.Range(2, 5);

            //Edge cases
            if (roomType == RoomType.BossRoom && isBossRoomGenerated)
                roomType = RoomType.NormalRoom;
            if (i + currentRooms == roomsCount && !isBossRoomGenerated)
                roomType = RoomType.BossRoom;
            if (roomType == RoomType.BossRoom)
                isBossRoomGenerated = true;
            if (i + currentRooms == roomsCount - 1 && !isPortalRoomGenerated)
                roomType = RoomType.PortalRoom;
            if (roomType == RoomType.PortalRoom && isPortalRoomGenerated)
                roomType = RoomType.NormalRoom;
            if (roomType == RoomType.PortalRoom)
                isPortalRoomGenerated = true;
            //

            int roomPosition = Random.Range(0, 4);

            switch (roomPosition)
            {
                case 0:
                    if (b + 1 < 7 && grid[a, b + 1] == RoomType.None)
                    {
                        grid[a, b + 1] = roomType;
                        AddRooms(a, b + 1);
                        continue;
                    }
                    break;
                case 1:
                    if (a + 1 < 7 && grid[a + 1, b] == RoomType.None)
                    {
                        grid[a + 1, b] = roomType;
                        AddRooms(a + 1, b);
                        continue;
                    }
                    break;
                case 2:
                    if (b - 1 >= 0 && grid[a, b - 1] == RoomType.None)
                    {
                        grid[a, b - 1] = roomType;
                        AddRooms(a, b - 1);
                        continue;
                    }
                    break;
                case 3:
                    if (a - 1 >= 0 && grid[a - 1, b] == RoomType.None)
                    {
                        grid[a - 1, b] = roomType;
                        AddRooms(a - 1, b);
                        continue;
                    }
                    break;
            }
            i--;
            if (roomType == RoomType.BossRoom)
                isBossRoomGenerated = false;
            if (roomType == RoomType.PortalRoom)
                isPortalRoomGenerated = false;
        }
    }

    private void DisplayDungeon()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++) 
            {
                if (grid[i, j] == RoomType.None)
                    continue;
                GameObject room = Instantiate(Resources.Load<GameObject>(RoomResourcesPath[grid[i, j]]), new Vector3(i * (378.8682f * 2), 0, j * (378.8682f * 2)), Quaternion.identity);
                allCameraPoints[i, j] = room.transform.Find("StartingCutscenePath").gameObject;
                if (j + 1 < 7 && grid[i, j + 1] != RoomType.None)
                    room.transform.Find("3").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (i + 1 < 7 && grid[i + 1, j] != RoomType.None)
                    room.transform.Find("4").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (j - 1 >= 0 && grid[i, j - 1] != RoomType.None)
                    room.transform.Find("1").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (i - 1 >= 0 && grid[i - 1, j] != RoomType.None)
                    room.transform.Find("2").GetComponent<ChangeWall>().ChangeToDoorWay();
            }
        }
    }

    public void GenerateStartingCutscenePath(int a, int b)
    {
        
        visitedRooms[a, b] = true;

        if (grid[a, b] == RoomType.BossRoom)
        {
            if (currPathLength < minPathLength)
            {
                minPathLength = currPathLength;
                cameraPoints = currCameraPoints;
            }
        }
        else
        {
            currPathLength++;
            currCameraPoints.Add(allCameraPoints[a, b]);
            if (b + 1 < 7 && grid[a, b + 1] != RoomType.None && !visitedRooms[a, b + 1])
                GenerateStartingCutscenePath(a, b + 1);
            if (a + 1 < 7 && grid[a + 1, b] != RoomType.None && !visitedRooms[a + 1, b])
                GenerateStartingCutscenePath(a + 1, b);
            if (b - 1 >= 0 && grid[a, b - 1] != RoomType.None && !visitedRooms[a, b - 1])
                GenerateStartingCutscenePath(a, b - 1);
            if (a - 1 >= 0 && grid[a - 1, b] != RoomType.None && !visitedRooms[a - 1, b])
                GenerateStartingCutscenePath(a - 1, b);
            currPathLength--;
            currCameraPoints.Remove(allCameraPoints[a, b]);
        }

        visitedRooms[a, b] = false;
    }
}
