using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Klasa przechowująca informacje o pokoju

public class RoomInfo
{
    public int x;
    public int y;
    public int type;
    public bool upDoor, downDoor, leftDoor, rightDoor;
}

//Klasa obsługująca wszystkie pokoje wygenerowane przez algorytm

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
    void Start()
    {
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    //Ładowanie pokoi na bieżąco (w momencie gdy jakieś znajdują sie w kolejce do załadowania)
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        //Gdy wszystkie pokoje są załadowane, wtedy przechodzimy do monitorowania pokoi
        if (loadRoomQueue.Count == 0)
        {
            UpdateRooms();
            return;
        }
        
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    //Załadowanie pokoju do kolejki
    public void LoadRoom(Room room, int x, int y)
	{
        if (DoesRoomExist(x,y))
        {
            return;
        }
		RoomInfo newRoomInfo = new RoomInfo();
        newRoomInfo.upDoor = room.upDoor;
        newRoomInfo.downDoor = room.downDoor;
        newRoomInfo.leftDoor = room.leftDoor;
        newRoomInfo.rightDoor = room.rightDoor;
        newRoomInfo.type = room.type;
        newRoomInfo.x = x;
        newRoomInfo.y = y;
        loadRoomQueue.Enqueue(newRoomInfo);
	}

    //Załadowanie odpowiedniej sceny z pokojem (w zależności od ilości drzwi i typu)
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

    //Załadowanie danych do pojedynczego pokoju
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
        //Dodanie pokoju do listy załadowanych pokoi
		loadedRooms.Add(room);
	}

    //Sprawdzanie czy pokój już został załadowany
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }

    //Przemieszczenie kamery gdy gracz zmieni pokój
    public void OnPlayerEnterRoom(DungeonRoom room)
    {
        CameraControler.instance.currentRoom = room;
        currentRoom = room;

        UpdateRooms();
    }

    //Aktualizacja pokoi, w momencie gdy już wszystkie zostały załadowane
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
                    }
                }
            }
        }
    }
}
