using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTowerControler : MonoBehaviour
{
    private float lastFire;
    public float fireDelay;
    public EnemyControler enemyControler;
    public GameObject projectile;
    private bool shootNewProjectile = true;
    private GameObject player;
    private float delay = 0.73f;


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
            if (shootNewProjectile)
                StartCoroutine(StartDelay());
        }
    }

    IEnumerator StartDelay()
    {
        shootNewProjectile = false;
        yield return new WaitForSeconds(delay);
        if (Time.time > lastFire + fireDelay)
            {
                Shoot();
                lastFire = Time.time;
            }
        shootNewProjectile = true;
        if (delay == 0.73f)
            delay = 1.33f;
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
