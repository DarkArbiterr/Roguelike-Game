using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public float x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        if(RoomControler.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }
        RoomControler.instance.RegisterRoom(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(16,8,0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(x,y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomControler.instance.OnPlayerEnterRoom(this);
        }
    }
}
