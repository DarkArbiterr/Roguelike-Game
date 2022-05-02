using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa odpowiedzialna za zachowanie przeciwnika typu BigSlime

public class BigSlimeControler : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");       
    }

    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Hitbox")
        {
            player.GetComponent<PlayerControler>().DamagePlayer();
        }
    }
}
