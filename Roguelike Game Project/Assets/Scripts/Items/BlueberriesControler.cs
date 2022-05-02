using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "Blueberries"
//Zwiększa prędkość pocisków

public class BlueberriesControler : MonoBehaviour
{
    public PlayerControler playerControler;
    public string title = "Blueberries";
    public string description = "Bullet speed Up!";

    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>().SetupInfo(title, description);
        playerControler = GetComponent<PlayerControler>();
        playerControler.bulletSpeed += 50;
    }

    void Update()
    {
        
    }
}
