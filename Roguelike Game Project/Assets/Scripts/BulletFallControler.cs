using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa do spadajacej strzały

public class BulletFallControler : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.33f);
    }

    void Update()
    {
        
    }
}
