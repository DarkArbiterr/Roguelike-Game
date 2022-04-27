using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiadajaca za kontrolę pojedyńczego pokoju

public class DungeonRoom : MonoBehaviour
{
    public float x;
    public float y;
    public Transform[] objects;

    void Start()
    {
        if(RoomControler.instance == null)
        {
            return;
        }
        RoomControler.instance.RegisterRoom(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1280,720,0));
    }

    //Współrzędne środka pokoju
    public Vector3 GetRoomCentre()
    {
        return new Vector3(x,y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Movebox")
        {
            RoomControler.instance.OnPlayerEnterRoom(this);
        }
        //RoomControler.instance.OnPlayerEnterRoom(this);
    }
}
