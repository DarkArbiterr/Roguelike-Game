using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Klasa do obsługi generowania proceduralnego mapy

public class LevelGeneration : MonoBehaviour 
{
	int gridSizeX, gridSizeY;
	int numberOfRooms = 12;
	public List<Vector2> takenPositions = new List<Vector2>();
	Vector2 worldSize = new Vector2(4,4);
	public static Room[,] rooms;
	void Start() 
	{
		//Kontrola, czy nie próbujemy utworzyć więcej pokoi niż jest w stanie zmieścić się na siatce
		if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
		{ 
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
		}
		gridSizeX = Mathf.RoundToInt(worldSize.x); 
		gridSizeY = Mathf.RoundToInt(worldSize.y);
		CreateRooms(); 
		SetRoomDoors(); 
		SetItemRooms();
		DrawDungeon(); 
	}

	//Tworzenie pokoi
	void CreateRooms()
	{
		rooms = new Room[gridSizeX * 2,gridSizeY * 2];
		rooms[gridSizeX,gridSizeY] = new Room(Vector2.zero, 1);
		takenPositions.Insert(0,Vector2.zero);
		Vector2 checkPosition = Vector2.zero;

		for (int i =0; i < numberOfRooms -1; i++)
		{
			checkPosition = NewPosition();
			
			if (NumberOfNeighbors(checkPosition, takenPositions) > 1)
			{
				int iterations = 0;

				do
				{
					checkPosition = SelectiveNewPosition();
					iterations++;
				}
				while(NumberOfNeighbors(checkPosition, takenPositions) > 1 && iterations < 100);
			}
			rooms[(int) checkPosition.x + gridSizeX, (int) checkPosition.y + gridSizeY] = new Room(checkPosition, 0);
			takenPositions.Insert(0,checkPosition);
		}	
	}

	//Wybranie pozycji dla nowego pokoju
	Vector2 NewPosition()
	{
		int x = 0, y = 0;
		Vector2 checkingPosition = Vector2.zero;
		do
		{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
			x = (int) takenPositions[index].x;
			y = (int) takenPositions[index].y;
			bool axis = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);

			if (axis)
			{ 
				if (positive)
					y += 1;
				else
					y -= 1;
			}
			else
			{
				if (positive)
					x += 1;
				else
					x -= 1;
			}
			checkingPosition = new Vector2(x,y);
		}
		while (takenPositions.Contains(checkingPosition) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
		return checkingPosition;
	}

	//Druga metoda wyboru pozycji pokoju
	Vector2 SelectiveNewPosition()
	{
		int index = 0, i = 0;
		int x = 0, y = 0;
		Vector2 checkingPosition = Vector2.zero;
		do
		{
			i = 0;
			do
			{ 
				//Zamiast znaleźć puste pole sąsiadujące z losowym pokojem, zaczynamy z pokojem który ma
				//dokładnie jednego sąsiada, dzięki temu częściej dostaniemy pokój który się rozgałęzia
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				i++;
			}
			while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && i < 100);

			x = (int) takenPositions[index].x;
			y = (int) takenPositions[index].y;
			bool axis = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);

			if (axis)
			{
				if (positive)
					y += 1;
				else
					y -= 1;
				
			}
			else
			{
				if (positive)
					x += 1;
				else
					x -= 1;
				
			}
			checkingPosition = new Vector2(x,y);
		}
		while (takenPositions.Contains(checkingPosition) 
				|| x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

		return checkingPosition;
	}

	//Zwraca ilość sąsiadów danej pozycji
	int NumberOfNeighbors(Vector2 checkingPosition, List<Vector2> usedPositions)
	{
		int ret = 0; 

		if (usedPositions.Contains(checkingPosition + Vector2.right))
			ret++;
		
		if (usedPositions.Contains(checkingPosition + Vector2.left))
			ret++;
		
		if (usedPositions.Contains(checkingPosition + Vector2.up))
			ret++;
		
		if (usedPositions.Contains(checkingPosition + Vector2.down))
			ret++;
		
		return ret;
	}

	//Wytworzenie dungeonu
	void DrawDungeon()
	{
		foreach (Room room in rooms)
		{
			if (room == null)
			{
				continue;
			}

			Vector2 drawPosition = room.gridPosition;
			drawPosition.x *= 1280;//aspect ratio of map sprite
			drawPosition.y *= 720;
			
			RoomControler.instance.LoadRoom(room, (int)drawPosition.x, (int)drawPosition.y);
		}
	}

	//Utworzenie pokoi z przedmiotami
	void SetItemRooms()
	{
		List<Room> validRooms = new List<Room>();
		
		foreach(Room room in rooms)
		{
			if (room == null){
				continue; 
			}

			if((room.leftDoor && !room.rightDoor && !room.upDoor && !room.downDoor && room.type == 0) ||
				(!room.leftDoor && room.rightDoor && !room.upDoor && !room.downDoor && room.type == 0) ||
				(!room.leftDoor && !room.rightDoor && room.upDoor && !room.downDoor && room.type == 0) ||
				(!room.leftDoor && !room.rightDoor && !room.upDoor && room.downDoor && room.type == 0))
			{
				validRooms.Add(room);
			}
		}

		int first, second;

		do
		{
			first = Random.Range(0, validRooms.Count);
			second = Random.Range(0, validRooms.Count);
		}
		while (first == second);

		validRooms[first].type = 2;
		validRooms[second].type = 2;

		for (int x = 0; x < ((gridSizeX * 2)); x++)
		{
			for (int y = 0; y < ((gridSizeY * 2)); y++)
			{
				if (rooms[x,y] == null){
					continue;
				}
				if(rooms[x,y].gridPosition == validRooms[first].gridPosition)
				{
					rooms[x,y] = validRooms[first]; 
				}
				if(rooms[x,y].gridPosition == validRooms[second].gridPosition)
				{
					rooms[x,y] = validRooms[second]; 
				}
			}
		}

	}

	//Ustawienie aktywnych drzwi dla każdego pokoju
	void SetRoomDoors()
	{
		for (int x = 0; x < ((gridSizeX * 2)); x++)
		{
			for (int y = 0; y < ((gridSizeY * 2)); y++)
			{
				if (rooms[x,y] == null)
					continue;
				
				Vector2 gridPosition = new Vector2(x,y);
				
				if (y - 1 < 0)
					rooms[x,y].downDoor = false;
				else
					rooms[x,y].downDoor = (rooms[x,y-1] != null);

				if (y + 1 >= gridSizeY * 2)
					rooms[x,y].upDoor = false;
				else
					rooms[x,y].upDoor = (rooms[x,y+1] != null);

				if (x - 1 < 0)
					rooms[x,y].leftDoor = false;
				else
					rooms[x,y].leftDoor = (rooms[x - 1,y] != null);

				if (x + 1 >= gridSizeX * 2)
					rooms[x,y].rightDoor = false;
				else
					rooms[x,y].rightDoor = (rooms[x+1,y] != null);
			}
		}
	}
}
