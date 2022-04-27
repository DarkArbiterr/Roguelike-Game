using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa do obsługi ścian

public class WallControler : MonoBehaviour
{
    void Start()
    {
        //Ignorowanie pszczególnych kolizji
        GameObject[] hitbox = GameObject.FindGameObjectsWithTag("Hitbox");
        Collider2D[] walls = GetComponents<Collider2D>();

        for (int i = 0; i < hitbox.Length; i++)
        {
            for (int j = 0; j < walls.Length; j++)
            {
                Physics2D.IgnoreCollision(hitbox[i].GetComponent<Collider2D>(), walls[j]);
            }
        }
        
    }
    void Update()
    {
        
    }
}
