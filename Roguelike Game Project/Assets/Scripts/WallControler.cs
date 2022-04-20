using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] hitbox = GameObject.FindGameObjectsWithTag("Hitbox");
        Collider2D[] walls = GetComponents<Collider2D>();

        for (int i = 0; i < hitbox.Length; i++)
        {
            for (int j = 0; j < walls.Length; j++)
            {
                Physics2D.IgnoreCollision(hitbox[i].GetComponent<Collider2D>(), walls[j]);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
