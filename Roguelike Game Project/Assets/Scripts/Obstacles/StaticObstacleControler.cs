using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa obsługująca statyczne elementy otoczenia

public class StaticObstacleControler : MonoBehaviour
{
    void Start()
    {
        //Ignorowanie określonych kolizji
        GameObject[] hitbox = GameObject.FindGameObjectsWithTag("Hitbox");

        for (int i = 0; i < hitbox.Length; i++)
        {
            Physics2D.IgnoreCollision(hitbox[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        
    }

    void Update()
    {  
    }
}
