using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Ogólna wspólna klasa do obsługi przedmiotów

public class ItemControler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Po wejściu w kolizję z danym przedmiotem, zostaje on dodany do obiektu gracza jako komponent
        if(collider.tag == "Movebox")
        {
            Debug.Log(gameObject.name + "Controler");
            string name = gameObject.name + "Controler";
            GameObject.FindGameObjectWithTag("Player").AddComponent(Type.GetType(name));
            Destroy(gameObject);
        }
    }
}
