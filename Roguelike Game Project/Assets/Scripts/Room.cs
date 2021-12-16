using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 gridPosition;
    public int type;
    public bool upDoor, downDoor, leftDoor, rightDoor;

    public Room(Vector2 GridPosition, int Type)
    {
        gridPosition = GridPosition;
        type = Type;
    }
}
