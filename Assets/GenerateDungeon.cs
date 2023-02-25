using UnityEngine;
public class GenerateDungeon : MonoBehaviour
{

    private int roomCount;
    private int currentRooms = 1;
    private bool isBossRoomGenerated = false;
    private bool isPortalRoomGenerated = false;
    private enum RoomType
    {
        None = 0,
        SafeRoom,
        NormalRoom,
        BossRoom,
        PortalRoom,
        Length
        
    }

    private RoomType[,] grid;
    void Start()
    { 
        roomCount = 6 + LoadDungeon.dungeonLevel * 2;
        grid = new RoomType[7,7];

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                grid[i, j] = RoomType.None;
            
        
        grid[4,4] = RoomType.SafeRoom;
        AddRooms(4, 4);
        DisplayDungeon();
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
                GameObject room = Instantiate(Resources.Load<GameObject>("DungeonRooms/Room"), new Vector3(i * (378.8682f * 2), 0, j * (378.8682f * 2)), Quaternion.identity);

                if (grid[i, j + 1] != RoomType.None)
                    room.transform.Find("3").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (grid[i + 1, j] != RoomType.None)
                    room.transform.Find("4").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (grid[i, j - 1] != RoomType.None)
                    room.transform.Find("1").GetComponent<ChangeWall>().ChangeToDoorWay();
                if (grid[i - 1, j] != RoomType.None)
                    room.transform.Find("2").GetComponent<ChangeWall>().ChangeToDoorWay();
            }
        }
    }
}
