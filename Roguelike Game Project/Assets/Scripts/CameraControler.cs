using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa do obsługi kamery

public class CameraControler : MonoBehaviour
{
    public static CameraControler instance;
    public DungeonRoom currentRoom;
    public float moveSpeedRoomChange;
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdatePosition();
    }
    
    //Nasłuchiwanie na zmianę pokoju, przy zmianie nastepuje przesunięcie kamery na dany pokój
    void UpdatePosition()
    {
        if (currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeedRoomChange);
    }
    //Pozyskanie pozycji nowego pokoju
    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = currentRoom.GetRoomCentre();
        targetPosition.z = transform.position.z;

        return targetPosition;
    }

}
