using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoomInfo
{
    public int x;
    public int y;
    public int type;
    public bool upDoor, downDoor, leftDoor, rightDoor;
}

public class RoomControler : MonoBehaviour
{
    public static RoomControler instance;
    string currentWorldName = "Wheatyard";
	Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    RoomInfo currentLoadRoomData;
    DungeonRoom currentRoom;
	public List<DungeonRoom> loadedRooms = new List<DungeonRoom>();
    bool isLoadingRoom = false;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            UpdateRooms();
            return;
        }
        
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(Room room, int x, int y)
	{
        if (DoesRoomExist(x,y))
        {
            return;
        }
		RoomInfo newRoomData = new RoomInfo();
        newRoomData.type = room.type;
        newRoomData.x = x;
        newRoomData.y = y;
        newRoomData.upDoor = room.upDoor;
        newRoomData.downDoor = room.downDoor;
        newRoomData.leftDoor = room.leftDoor;
        newRoomData.rightDoor = room.rightDoor;
        loadRoomQueue.Enqueue(newRoomData);
	}

	IEnumerator LoadRoomRoutine(RoomInfo room)
	{
        string path = "Scenes/RoomScenes/BasicRooms/UDLR/1";
        if (room.upDoor)
        {
            if (room.downDoor)
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UDLR/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UDL/1";
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UDR/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UD/1";
                    }
                }
            }
            else
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/URL/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UL/1";
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/UR/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/U/1";
                    }
                }
            }
        }
        else
        {
            if (room.downDoor)
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/DLR/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/LD/1";
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/RD/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/D/1";
                    }
                }
            }
            else
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/RL/1";
                    }
                    else
                    {
                        path = "Scenes/RoomScenes/BasicRooms/L/1";
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path = "Scenes/RoomScenes/BasicRooms/R/1";
                    }
                    else
                    {
                    }
                }
            }
        }

		AsyncOperation loadRoom = SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
		while(loadRoom.isDone == false)
		{
			yield return null;
		}
	}

	public void RegisterRoom(DungeonRoom room)
	{
		room.transform.position = new Vector3(
			currentLoadRoomData.x,
			currentLoadRoomData.y,
			0
		);
		
		room.x = currentLoadRoomData.x;
		room.y = currentLoadRoomData.y;
		room.name = currentWorldName + "-" + currentLoadRoomData.type + " " + room.x + "," + room.y;
        room.transform.parent = transform;
        isLoadingRoom = false;

        if (room.x == 0 && room.y == 0)
        {
            CameraControler.instance.currentRoom = room;
        }

		loadedRooms.Add(room);
	}

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }

    public void OnPlayerEnterRoom(DungeonRoom room)
    {
        CameraControler.instance.currentRoom = room;
        currentRoom = room;

        UpdateRooms();
    }

    public void UpdateRooms()
    {
        foreach(DungeonRoom room in loadedRooms)
        {
            if(currentRoom != room)
            {
                EnemyControler[] enemies = room.GetComponentsInChildren<EnemyControler>();
                if(enemies != null)
                {
                    foreach(EnemyControler enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        Debug.Log("Not in room");
                    }
                }
            }
            else
            {
                EnemyControler[] enemies = room.GetComponentsInChildren<EnemyControler>();
                if(enemies != null)
                {
                    foreach(EnemyControler enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        Debug.Log("In room");
                    }
                }
            }
        }
    }
}
