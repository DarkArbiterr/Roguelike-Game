using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za funkcjonowanie monety

public class CoinControler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Hitbox")
        {
            Debug.Log("Get a coin!");
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryControler>().AddCoin();
            Destroy(gameObject);
        }
    } 
}
