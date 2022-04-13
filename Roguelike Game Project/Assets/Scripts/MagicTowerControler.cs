using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTowerControler : MonoBehaviour
{
    private float lastFire;
    public float fireDelay;
    public EnemyControler enemyControler;
    public GameObject projectile;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyControler = GetComponentInParent<EnemyControler>();
        if(enemyControler.notInRoom == false)
        {
            if (Time.time > lastFire + fireDelay)
            {
                Shoot();
                lastFire = Time.time;
            }
            
        }
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
