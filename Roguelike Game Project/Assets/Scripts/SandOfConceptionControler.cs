using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "SandOfConception"
//Zwiększa częstotliwość wystrzeliwanych strzał

public class SandOfConceptionControler : MonoBehaviour
{
    public PlayerControler playerControler;
    public string title = "Sand Of Conception";
    public string description = "Periodicity Up!";

    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        playerControler.fireDelay -= 0.1f;
    }

    void Update()
    {
        
    }
}
