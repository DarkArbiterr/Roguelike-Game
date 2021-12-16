using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoomInfo
{
    public int x;
    public int y;
    public int type;
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

    public void LoadRoom(int type, int x, int y)
	{
        if (DoesRoomExist(x,y))
        {
            return;
        }
		RoomInfo newRoomData = new RoomInfo();
        newRoomData.type = type;
        newRoomData.x = x;
        newRoomData.y = y;
        loadRoomQueue.Enqueue(newRoomData);
	}

	IEnumerator LoadRoomRoutine(RoomInfo room)
	{
		AsyncOperation loadRoom = SceneManager.LoadSceneAsync("RoomTemplate", LoadSceneMode.Additive);
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

        if (loadedRooms.Count == 0)
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

    private void UpdateRooms()
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
