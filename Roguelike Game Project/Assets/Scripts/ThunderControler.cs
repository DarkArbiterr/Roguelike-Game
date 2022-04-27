using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "Thunder"
//Zwiększa prędkość gracza

public class ThunderControler : MonoBehaviour
{
    public PlayerControler playerControler;
    public string title = "Thunder";
    public string description = "Speed Up!";

    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        playerControler.moveSpeed += 20;
    }
    void Update()
    {
    }
}
