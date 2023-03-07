using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class GenerateDungeon : MonoBehaviour
{
    private int roomCount;
    private int currentRooms = 1;
    private bool isBossRoomGenerated = false;
    private bool isPortalRoomGenerated = false;
    [SerializeField] GameObject cinematicCamera;
    public static int dungeonLevel = 0;


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
    public static List<GameObject> cameraPoints;//
    public static List<int> rotationList;
    public static List<int> currRotationList = new List<int>();
    private GameObject[,] allCameraPoints;
    void Start()
    {
        RoomResourcesPath = new Dictionary<RoomType, string>() {
            { RoomType.SafeRoom, "DungeonRooms/SafeRoom" },
            { RoomType.NormalRoom, "DungeonRooms/NormalRoom" },
            { RoomType.BossRoom, "DungeonRooms/BossRoom" },
            { RoomType.PortalRoom, "DungeonRooms/PortalRoom" }
        };


        roomCount = 6 + GenerateDungeon.dungeonLevel * 2;
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
        cinematicCamera.GetComponent<MoveTowardsBoss>().enabled = true;
    }

    private void AddRooms(int x, int y)
    {
        if (roomCount - currentRooms == 0)
            return;

        int availableRoomsCount = 0;

        for (int i = 0; i < 4; i++)
        {
            int newX = x + i - 1;
            newX = i == 3 ? x : newX;
            int newY = y + i % 2 * (i > 2 ? -1 : 1);
            if (newX >= 0 && newX < 7 && newY >= 0 && newY < 7 && grid[newX, newY] == RoomType.None)
                availableRoomsCount++;
        }

        if (availableRoomsCount == 0)
            return;

        int roomsCount = Random.Range(1, Mathf.Min(availableRoomsCount, roomCount - currentRooms));
        currentRooms += roomsCount;
        for (int i = 0; i < roomsCount; i++) 
        {
            RoomType roomType = (RoomType)Random.Range(2, (int)RoomType.Length);

            //Edge cases
            if (roomType == RoomType.BossRoom && isBossRoomGenerated)
                roomType = RoomType.NormalRoom;
            if (roomType == RoomType.PortalRoom && isPortalRoomGenerated)
                roomType = RoomType.NormalRoom;

            if (currentRooms == roomCount && !isBossRoomGenerated)
                roomType = RoomType.BossRoom;
            if (currentRooms == roomCount - 1 && !isPortalRoomGenerated)
                roomType = RoomType.PortalRoom;

            if (roomType == RoomType.BossRoom)
                isBossRoomGenerated = true;
            if (roomType == RoomType.PortalRoom)
                isPortalRoomGenerated = true;
            //

            int roomPosition = Random.Range(0, 4);
            int newX = x + roomPosition - 1;
            newX = roomPosition == 3 ? x : newX;
            int newY = y + roomPosition % 2 * (roomPosition > 2 ? -1 : 1);
            if (newX >= 0 && newX < 7 && newY >= 0 && newY < 7 && grid[newX, newY] == RoomType.None)
            {
                grid[newX, newY] = roomType;
                AddRooms(newX, newY);
                continue;
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

                for (int t = 0; t < 4; t++)
                {
                    int newX = i + t - 1;
                    newX = t == 3 ? i : newX;
                    int newY = j + t % 2 * (t > 2 ? -1 : 1);
                    if (newX >= 0 && newX < 7 && newY >= 0 && newY < 7 && grid[newX, newY] != RoomType.None)
                        room.transform.Find(t.ToString()).GetComponent<ChangeWall>().ChangeToDoorWay();
                }

            }
        }
    }

    public void GenerateStartingCutscenePath(int a, int b)
    {
        visitedRooms[a, b] = true;

        if (grid[a, b] == RoomType.BossRoom)
        {
            currCameraPoints.Add(allCameraPoints[a, b]);
            if (currPathLength < minPathLength)
            {
                minPathLength = currPathLength;
                cameraPoints = new List<GameObject>(currCameraPoints);
                rotationList = new List<int>(currRotationList);
            }
            currCameraPoints.Remove(allCameraPoints[a, b]);
        }
        else
        {
            currPathLength++;
            currCameraPoints.Add(allCameraPoints[a, b]);

            if (b + 1 < 7 && grid[a, b + 1] != RoomType.None && !visitedRooms[a, b + 1])
            {
                currRotationList.Add(3);
                GenerateStartingCutscenePath(a, b + 1);
                currRotationList.RemoveAt(currRotationList.FindLastIndex(x => x == 3));
            }
            if (a + 1 < 7 && grid[a + 1, b] != RoomType.None && !visitedRooms[a + 1, b])
            {
                currRotationList.Add(4);
                GenerateStartingCutscenePath(a + 1, b);
                currRotationList.RemoveAt(currRotationList.FindLastIndex(x => x == 4));
            }
            if (b - 1 >= 0 && grid[a, b - 1] != RoomType.None && !visitedRooms[a, b - 1])
            {
                currRotationList.Add(1);
                GenerateStartingCutscenePath(a, b - 1);
                currRotationList.RemoveAt(currRotationList.FindLastIndex(x => x == 1));
            }
            if (a - 1 >= 0 && grid[a - 1, b] != RoomType.None && !visitedRooms[a - 1, b])
            {
                currRotationList.Add(2);
                GenerateStartingCutscenePath(a - 1, b);
                currRotationList.RemoveAt(currRotationList.FindLastIndex(x => x == 2));
            }

            currPathLength--;
            currCameraPoints.Remove(allCameraPoints[a, b]);
        }

        visitedRooms[a, b] = false;
    }
    /* private bool isBossRoomVisited = false;
     public static List<GameObject> cameraPoints;*/


    /*public void GenerateStartingCutscenePath(int x, int y)
    {
        List<GameObject> parent = new List<GameObject>();
        List<GameObject> currObject = new List<GameObject>();
        List<int> queueX = new List<int>();
        List<int> queueY = new List<int>();

        int nextIndex = 0;

        while (!isBossRoomVisited)
        {
            for (int i = 0; i < 4; i++)
            {
                int newX = x + i - 1;
                newX = i == 3 ? x : newX;
                int newY = y + i % 2 * (i > 2 ? -1 : 1);
                if (!(newX >= 0 && newX < 7 && newY >= 0 && newY < 7))//out of range
                    continue;

                if (grid[newX, newY] == RoomType.BossRoom)
                {
                    isBossRoomVisited = true;
                    break;
                }

                if (grid[newX, newY] == RoomType.None)
                    continue;

                queueX.Add(newX);
                queueY.Add(newY);
            }
            parent.Add(allCameraPoints[x, y]);
            x = queueX.ElementAt(nextIndex);
            y = queueY.ElementAt(nextIndex);
            currObject.Add(allCameraPoints[x, y]);
            Debug.Log(currObject.Count);

            nextIndex++;
        }
        nextIndex--;
        while (true)
        {
            Debug.Log(nextIndex +" " + currObject.Count);
            GameObject obj = currObject.ElementAt(nextIndex);
            cameraPoints.Add(obj);
            if (obj == allCameraPoints[4, 4]) //starting room
                break;
            nextIndex = currObject.FindIndex(x => x == parent.ElementAt(nextIndex));
        }
        cameraPoints.Reverse();
    }*/
}
