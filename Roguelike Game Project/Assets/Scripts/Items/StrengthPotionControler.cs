using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "Strength Potion"
//Zwiększa obrażenia

public class StrengthPotionControler : MonoBehaviour
{
    public PlayerControler playerControler;
    public string title = "Strength Potion";
    public string description = "Damage Up!";
    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>().SetupInfo(title, description);
        playerControler = GetComponent<PlayerControler>();
        playerControler.damage += 3;
    }

    void Update()
    {
    }
}
