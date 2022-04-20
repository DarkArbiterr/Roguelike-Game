using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] hitbox = GameObject.FindGameObjectsWithTag("Hitbox");

        for (int i = 0; i < hitbox.Length; i++)
        {
            Physics2D.IgnoreCollision(hitbox[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
