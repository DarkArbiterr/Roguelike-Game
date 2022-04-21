using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ItemControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.tag == "Movebox")
        {
            Debug.Log(gameObject.name + "Controler");
            string name = gameObject.name + "Controler";
            GameObject.FindGameObjectWithTag("Player").AddComponent(Type.GetType(name));
            Destroy(gameObject);
        }
    }
}
