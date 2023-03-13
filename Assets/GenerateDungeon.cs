using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

public class GenerateDungeon : MonoBehaviour
{
    private int roomCount;
    private int currentRooms = 1;
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

    public static List<GameObject> cameraPoints = new();//
    public static List<int> rotationList;
    public static List<int> currRotationList = new List<int>();

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

        roomCount = 6 + dungeonLevel * 2;

        Room safeRoom = new Room(4, 4, RoomType.SafeRoom);
        AddRooms(safeRoom);
        OpenDoors(safeRoom);
        GenerateStartingCutscenePath(safeRoom);
        cinematicCamera.GetComponent<MoveTowardsBoss>().enabled = true;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().enabled = true;
        GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>().setCinemacticCamera(cinematicCamera);
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
                newRoom.adjacent[roomPosition > 1 ? roomPosition - 2 : roomPosition + 2] = room;
                room.adjacent[roomPosition] = newRoom;
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
            if (startingRoom.adjacent[i] == null || startingRoom.adjacent[i].isVisited)
                continue;

            startingRoom.cameraPosObj.transform.parent.Find(i.ToString()).GetComponent<ChangeWall>().ChangeToDoorWay();
            startingRoom.adjacent[i].cameraPosObj.transform.parent.Find((i > 1 ? i - 2 : i + 2).ToString()).GetComponent<ChangeWall>().ChangeToDoorWay();
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
    public static List<Room> visited = new();

    public void GenerateStartingCutscenePath(Room startingRoom)
    {
        Room safeRoom = startingRoom;
        queue.Add(startingRoom);
        while (!isBossRoomVisited)
        {
            startingRoom = queue.First();
            queue.Remove(startingRoom);
            visited.Add(startingRoom);
            for (int i = 0; i < 4; i++)
            {
                if (startingRoom.adjacent[i] == null)
                    continue;

                if (startingRoom.adjacent[i].roomType == RoomType.BossRoom)
                {
                    visited.Add(startingRoom.adjacent[i]);
                    isBossRoomVisited = true;
                    break;
                }
                queue.Add(startingRoom.adjacent[i]);
            }
        }
        cameraPoints = new List<GameObject>();
        while (cameraPoints.Find(x => x == safeRoom.cameraPosObj) == null)
        {
            Room curr = visited.Last();
            cameraPoints.Add(curr.cameraPosObj);
            visited.Remove(visited.ElementAt(visited.Count-1));
            while (visited.Last() != null && !visited.Last().FindAdjacent(curr))
                visited.Remove(visited.ElementAt(visited.Count - 1));
        }
        cameraPoints.Reverse();
    }
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
        FindAvailable(); //set adjacent
    }

    public Room FindRoom(int x, int y)
    {
        Room result = null;
        isVisited = true;
        for (int i = 0; i < 4; i++)
        {
            if (adjacent[i] == null || adjacent[i].isVisited)
                continue;
            if (adjacent[i].x == x && adjacent[i].y == y)
                result = adjacent[i];
            Room findRoomRes = adjacent[i].FindRoom(x, y);
            if (findRoomRes != null)
                result = findRoomRes;
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
            {
                room.adjacent[i > 1 ? i - 2 : i + 2] = adjacent[i];
                adjacent[i] = room;
            }
        }
        return result;
    }
    public bool FindAdjacent(Room room)
    {
        for (int i = 0; i < 4; i++)
        {
            if (adjacent[i] == room)
                return true;
        }
        return false;
    }
}