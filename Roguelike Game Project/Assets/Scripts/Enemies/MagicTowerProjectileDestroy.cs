using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa do rozpadajacego się pocisku

public class MagicTowerProjectileDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.33f);
    }

    void Update()
    {
        
    }
}
