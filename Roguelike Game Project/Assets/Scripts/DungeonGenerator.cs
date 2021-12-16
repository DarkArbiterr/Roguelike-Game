using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{/*
    Vector2 levelSize = new Vector2(4, 4);
    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 20;
    public GameObject roomObj;
    public Transform mapRoot;

    // Start is called before the first frame update
    void Start()
    {
        // Sprawdzenie czy okreslona liczba pokoi miesci sie na siatce, jeœli nie, dopasowanie ilosci pokoi do siatki
        if (numberOfRooms >= (levelSize.x * 2) * (levelSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((levelSize.x * 2) * (levelSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(levelSize.x);
        gridSizeY = Mathf.RoundToInt(levelSize.y);
        CreateRooms();
        SetRoomDoors();
        GenerateMap();
    }

    void GenerateMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue;
            }
            Vector2 drawPosition = room.gridPosition;
            drawPosition.x *= 16;
            drawPosition.y *= 8;
            MapSpriteSelector mapper = Object.Instantiate(roomObj, drawPosition, Quaternion.identity).GetComponent<MapSpriteSelector>();
            mapper.type = room.type;
            mapper.up = room.upDoor;
            mapper.down = room.downDoor;
            mapper.left = room.leftDoor;
            mapper.right = room.rightDoor;
            mapper.gameObject.transform.parent = mapRoot;
        } 
    }

    void CreateRooms()
    {
        // Ustawienie zmiennych
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPosition = Vector2.zero;
        // Numery odpowiedzialne za wieksze 'rozproszenie' mapy
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        // Dodawanie pokoi
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomVariable = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomVariable);
            // Wziêcie nowej pozycji
            checkPosition = NewPosition();
            // Testowanie nowej pozycji
            if (NumberOfNeighbors(checkPosition, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterator = 0;
                do
                {
                    checkPosition = SelectiveNewPosition();
                    iterator++;
                }
                while (NumberOfNeighbors(checkPosition, takenPositions) > 1 && iterator < 100);
                if (iterator >= 50)
                    print("nie mozna utworzyc z mniejsza iloscia sasiadow ni¿ : " + NumberOfNeighbors(checkPosition, takenPositions));
                // Finzalizacja pozycji
                rooms[(int)checkPosition.x + gridSizeX, (int)checkPosition.y + gridSizeY] = new Room(checkPosition, 0);
                takenPositions.Insert(0, checkPosition);
            }
        }


    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPosition = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPosition = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPosition) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        return checkingPosition;

    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPosition = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            }
            while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPosition = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPosition) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        {
            print("nie mozna znalezc pozycji z tylko jednym s¹siadem");
        }
        return checkingPosition;

    }

    int NumberOfNeighbors(Vector2 checkingPosition, List<Vector2> usedPositions)
    {
        int value = 0;
        if (usedPositions.Contains(checkingPosition + Vector2.right))
        {
            value++;
        }
        if (usedPositions.Contains(checkingPosition + Vector2.left))
        {
            value++;
        }
        if (usedPositions.Contains(checkingPosition + Vector2.up))
        {
            value++;
        }
        if (usedPositions.Contains(checkingPosition + Vector2.down))
        {
            value++;
        }
        return value;
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                if (rooms[x,y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                // Sprawdzanie czy utworzyc dolne drzwi
                if (y - 1 < 0)
                {
                    rooms[x, y].downDoor = false;
                }
                else
                {
                    rooms[x, y].downDoor = (rooms[x, y - 1] != null);
                }
                // Sprawdzanie czy utworzyc górne drzwi
                if (y + 1 >= gridSizeY * 2)
                {
                    rooms[x, y].upDoor = false;
                }
                else
                {
                    rooms[x, y].upDoor = (rooms[x, y + 1] != null);
                }
                // Sprawdzanie czy utworzyc lewe drzwi
                if (x - 1 < 0)
                {
                    rooms[x, y].leftDoor = false;
                }
                else
                {
                    rooms[x, y].leftDoor = (rooms[x - 1, y] != null);
                }
                // Sprawdzanie czy utworzyc prawe drzwi
                if (x + 1 >= gridSizeX * 2)
                {
                    rooms[x, y].rightDoor = false;
                }
                else
                {
                    rooms[x, y].rightDoor = (rooms[x + 1, y] != null);
                }
            }
        }
    }*/
}
