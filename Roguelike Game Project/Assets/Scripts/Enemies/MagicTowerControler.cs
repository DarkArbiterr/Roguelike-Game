using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa obsługująca zachowanie przeciwnika "MagicTowerController"

public class MagicTowerControler : MonoBehaviour
{
    private float lastFire;
    public float fireDelay;
    public EnemyControler enemyControler;
    public GameObject projectile;
    private bool shootNewProjectile = true;
    private GameObject player;
    private float delay = 0.73f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        EnemyBehaviour();
    }

    //Zachowanie przeciwnika MagicTower (strzelanie w stronę przeciwnika)
    public void EnemyBehaviour()
    {
        enemyControler = GetComponentInParent<EnemyControler>();
        if(enemyControler.notInRoom == false)
        {
            if (shootNewProjectile)
                StartCoroutine(StartDelay());
        }
    }

    //Opóźnienie z jakim wystrzeliwane są pociski
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Hitbox")
        {
            player.GetComponent<PlayerControler>().DamagePlayer();
        }
    }

    //Wystrzelenie pocisku w kierunku gracza
    void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    
}
