using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za funkcjonowanie pocisku gracza

public class BulletControler : MonoBehaviour
{
    public float lifeTime;
    public float pushPower;
    public float damage;
    public GameObject fallEffect;

    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    void Update()
    {
        
    }

    //Zniszczenie strzały po danym czasie (i utworzenie nowego obiektu z animacją opadania)
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Instantiate(fallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if(colider.tag == "Enemy")
        {
            //Kod odpowiedzialny za dodanie odrzutu przeciwnika po trafieniu
            bool isKnockback = colider.GetComponent<EnemyControler>().isKnockback;
            Rigidbody2D enemy = colider.GetComponent<Rigidbody2D>();
            if (isKnockback)
            {
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * pushPower;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                
            }
            Destroy(gameObject);
            colider.gameObject.GetComponent<EnemyControler>().Hit(damage);
            
        }
        if(colider.tag == "WallBulletCollider" || colider.tag == "Obstacle" || colider.tag == "Room")
        {
            Instantiate(fallEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
