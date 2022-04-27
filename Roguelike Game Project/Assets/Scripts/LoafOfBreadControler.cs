using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za działanie przedmiotu "Loaf Of Bread"
//Zwiększa ilość serduszek o 2 i w pełni uzdrawia

public class LoafOfBreadControler : MonoBehaviour
{
    public HealthControler healthControler;
    public string title = "Loaf Of Bread";
    public string description = "Double Health Up!";

    void Start()
    {
        healthControler = GetComponent<HealthControler>();
        healthControler.numberOfHearts += 2;
        healthControler.health = healthControler.numberOfHearts;
    }

    void Update()
    {
        
    }
}
