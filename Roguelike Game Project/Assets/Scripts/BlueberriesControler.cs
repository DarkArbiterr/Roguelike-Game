using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "Blueberries"
//Zwiększa prędkość pocisków

public class BlueberriesControler : MonoBehaviour
{
    public PlayerControler playerControler;
    public string title = "Blueberries";
    public string description = "Speed Up!";

    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        playerControler.bulletSpeed += 50;
    }

    void Update()
    {
        
    }
}
