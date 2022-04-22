using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControler : MonoBehaviour
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
        if(collider.tag == "Hitbox")
        {
            Debug.Log("Get a coin!"); 
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryControler>().AddCoin();
            Destroy(gameObject);
        }
    } 
}
