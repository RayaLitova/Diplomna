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


    public enum RoomType
    {
        None = 0,
        SafeRoom,
        NormalRoom,
        BossRoom,
        PortalRoom,
        Length
    }

    public static Dictionary<RoomType, string> RoomResourcesPath;

    private RoomType[,] grid;

    private int minPathLength;
    private int currPathLength = 0;
    private bool[,] visitedRooms;
    private List<GameObject> currCameraPoints = new List<GameObject>();
    public static List<GameObject> cameraPoints;//
    public static List<int> rotationList;
    public static List<int> currRotationList = new List<int>();
    private GameObject[,] allCameraPoints;
    public static int bossRoomNum = -1;
    public static int portalRoomNum = -1;
    void Start()
    {
        RoomResourcesPath = new Dictionary<RoomType, string>()
        {
            { RoomType.SafeRoom, "DungeonRooms/SafeRoom" },
            { RoomType.NormalRoom, "DungeonRooms/NormalRoom" },
            { RoomType.BossRoom, "DungeonRooms/BossRoom" },
            { RoomType.PortalRoom, "DungeonRooms/PortalRoom" }
        };


        roomCount = 6 + GenerateDungeon.dungeonLevel * 2;
        minPathLength = roomCount; //biggest possible length
        grid = new RoomType[7, 7];

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                grid[i, j] = RoomType.None;


        grid[4, 4] = RoomType.SafeRoom;

        visitedRooms = new bool[7, 7];

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                visitedRooms[i, j] = false;

        allCameraPoints = new GameObject[7, 7];

        Room safeRoom = new Room(4, 4, RoomType.SafeRoom);
        AddRooms(safeRoom);
        OpenDoors(safeRoom);
        GenerateStartingCutscenePath(safeRoom);
        /*DisplayDungeon();
        GenerateStartingCutscenePath(4, 4);
        cinematicCamera.GetComponent<MoveTowardsBoss>().enabled = true;*/
    }

    private void AddRooms(Room room)
    {
        int availableRooms = room.FindAvailable(); // fill adjacent

        if (currentRooms == roomCount)
            return;
        if (bossRoomNum == -1)
        {
            bossRoomNum = Random.Range(1, roomCount);
            portalRoomNum = Random.Range(1, roomCount);
            if (bossRoomNum == portalRoomNum)
                portalRoomNum--;
        }
        if (availableRooms == 0)
            return;

        int roomsCount = Random.Range(1, Mathf.Min(availableRooms, roomCount - currentRooms));
        int currRoomsCopy = currentRooms;
        currentRooms += roomsCount;
        for (int i = 0; i < roomsCount; i++)
        {
            RoomType roomType = RoomType.NormalRoom;
            if (currRoomsCopy + i == bossRoomNum)
                roomType = RoomType.BossRoom;
            if (currRoomsCopy + i == portalRoomNum)
                roomType = RoomType.PortalRoom;

            int roomPosition = Random.Range(0, 4);
            int newX = room.x + roomPosition - 1;
            newX = roomPosition == 3 ? room.x : newX;
            int newY = room.y + roomPosition % 2 * (roomPosition > 2 ? -1 : 1);
            if (newX >= 0 && newX < 7 && newY >= 0 && newY < 7 && room.adjacent[roomPosition] == null)
            {
                Room newRoom = new(newX, newY, roomType);
                room.adjacent[roomPosition] = newRoom;
                newRoom.adjacent[roomPosition > 1 ? roomPosition - 2 : roomPosition + 2] = room;
                AddRooms(newRoom);
                continue;
            }
            i--;
        }


    }

    public static void OpenDoors(Room startingRoom)
    {
        startingRoom.isVisited = true;
        for (int i = 0; i < 4; i++)
        {
            if (startingRoom.adjacent[i] == null)
                continue;
            startingRoom.cameraPosObj.transform.parent.Find(i.ToString()).GetComponent<ChangeWall>().ChangeToDoorWay();
            if (!startingRoom.adjacent[i].isVisited)
                OpenDoors(startingRoom.adjacent[i]);
        }
        startingRoom.isVisited = false;
    }

    public static void DisplayRoom(Room room)
    {
        GameObject newRoom = Instantiate(Resources.Load<GameObject>(RoomResourcesPath[room.roomType]), new Vector3(room.x * (378.8682f * 2), 0, room.y * (378.8682f * 2)), Quaternion.identity);
        room.cameraPosObj = newRoom.transform.Find("StartingCutscenePath").gameObject;
    }


    private bool isBossRoomVisited = false;
    public static List<Room> queue = new();

    public void GenerateStartingCutscenePath(Room startingRoom)
    {
        queue.Add(startingRoom);
        while (queue.Count != 0 && !isBossRoomVisited)
        {
            for (int i = 0; i < 4; i++)
            {
                if (startingRoom.adjacent[i] == null)
                    continue;

                if (startingRoom.adjacent[i].roomType == RoomType.BossRoom)
                {
                    Debug.Log("aaaaaa");
                    isBossRoomVisited = true;
                    break;
                }
                Debug.Log("Add room");
                queue.Add(startingRoom.adjacent[i]);
            }
            return;
        }

    }

    /*    public void GenerateStartingCutscenePath(int x, int y)
        {
            List<GameObject> parent = new();
            List<GameObject> currObject = new();
            List<int> queueX = new();
            List<int> queueY = new();

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
                    parent.Add(allCameraPoints[x, y]);
                }
                x = queueX.ElementAt(nextIndex);
                y = queueY.ElementAt(nextIndex);
                currObject.Add(allCameraPoints[x, y]);

                nextIndex++;
            }
            nextIndex--;

            cameraPoints.Add(currObject.ElementAt(nextIndex));
            while (true)
            {
                GameObject obj = parent.ElementAt(nextIndex);
                cameraPoints.Add(obj);
                if (obj == allCameraPoints[4, 4]) //starting room
                    break;
                nextIndex = currObject.FindIndex(x => x == obj);
            }
            cameraPoints.Reverse();
        }
    }*/
}

public class Room
{
    public int x;
    public int y;
    public GameObject cameraPosObj;
    public Room[] adjacent;
    public GenerateDungeon.RoomType roomType;

    public bool isVisited = false;

    public Room()
    {
        x = 0;
        y = 0;
        cameraPosObj = null;
        adjacent = new Room[4];
        roomType = GenerateDungeon.RoomType.None;
    }
    public Room(int x, int y, GenerateDungeon.RoomType roomType)
    {
        this.x = x;
        this.y = y;
        adjacent = new Room[4];
        this.roomType = roomType;
        GenerateDungeon.DisplayRoom(this);
    }

    public Room FindRoom(int x, int y) //ne raboti
    {
        Room result = null;
        isVisited = true;
        for (int i = 0; i < 4; i++)
        {
            if (adjacent[i] == null || adjacent[i].isVisited)
                continue;
            if (adjacent[i].x == x && adjacent[i].y == y)
                result = adjacent[i];
            if (adjacent[i].FindRoom(x, y) != null)
                result = adjacent[i].FindRoom(x, y);
        }
        isVisited = false;
        return result;
    }

    public int FindAvailable()
    {
        int result = 0;
        for (int i = 0; i < 4; i++)
        {
            if (adjacent[i] != null)
                continue;
            int newX = x + i - 1;
            newX = i == 3 ? x : newX;
            int newY = y + i % 2 * (i > 2 ? -1 : 1);
            Room room = FindRoom(newX, newY);
            if (room == null)
                result++;
            else
                adjacent[i] = room;
        }
        return result;
    }
}