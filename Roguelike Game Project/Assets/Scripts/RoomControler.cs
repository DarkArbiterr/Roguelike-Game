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
        int r = Random.Range(1,6);
        string path = "Scenes/RoomScenes/BasicRooms/";

        if (room.type == 1)
        {
            path = "Scenes/RoomScenes/StartingRooms/";
            r = 1;
        }

        if (room.type == 2)
        {
            path = "Scenes/RoomScenes/ItemRooms/";
            r = 1;
        }   

        if (room.upDoor)
        {
            if (room.downDoor)
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path += "UDLR/" + r.ToString();
                    }
                    else
                    {
                        path += "UDL/" + r.ToString();
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path += "UDR/" + r.ToString();
                    }
                    else
                    {
                        path += "UD/" + r.ToString();
                    }
                }
            }
            else
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path += "URL/" + r.ToString();
                    }
                    else
                    {
                        path += "UL/" + r.ToString();
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path += "UR/" + r.ToString();
                    }
                    else
                    {
                        path += "U/" + r.ToString();
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
                        path += "DLR/" + r.ToString();
                    }
                    else
                    {
                        path += "LD/" + r.ToString();
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path += "RD/" + r.ToString();
                    }
                    else
                    {
                        path += "D/" + r.ToString();
                    }
                }
            }
            else
            {
                if (room.leftDoor)
                {
                    if (room.rightDoor)
                    {
                        path += "RL/" + r.ToString();
                    }
                    else
                    {
                        path += "L/" + r.ToString();
                    }
                }
                else
                {
                    if (room.rightDoor)
                    {
                        path += "R/" + r.ToString();
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
                        //Debug.Log("Not in room");
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
                        //Debug.Log("In room");
                    }
                }
            }
        }
    }
}
