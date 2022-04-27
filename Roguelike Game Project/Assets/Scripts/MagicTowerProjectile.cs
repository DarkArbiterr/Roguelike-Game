using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa obsługująca pociski wystrzeliwane przez Magic Tower

public class MagicTowerProjectile : MonoBehaviour
{
    public float lifeTime;
    private GameObject player;
    private Vector2 target;
    private Rigidbody2D rb;
    public GameObject projectileDestroy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ProjectileMove();
        }
        
    }

    //Miejsce do którego ma lecieć pocisk wystrzelony przez przeciwnika
    public void ProjectileMove()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                player.transform.position.x - transform.position.x,
                player.transform.position.y - transform.position.y).normalized * 400;
            StartCoroutine(DeathDelay());
    }

    //Czas po którym pocisk zostanie zniszczony
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Instantiate(projectileDestroy, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if(colider.tag == "WallBulletCollider" || colider.tag == "Obstacle" || colider.tag == "Room")
        {
            Instantiate(projectileDestroy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(colider.tag == "Hitbox")
        {
            player.GetComponent<PlayerControler>().DamagePlayer();
            Instantiate(projectileDestroy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
