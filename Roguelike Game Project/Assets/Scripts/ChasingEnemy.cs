using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa odpowiedzialna za zachowanie przeciwnika typu ChasingEnemy

public class ChasingEnemy : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    private bool coolDownAttack;
    private bool facingRight = true;
    public float coolDown;
    public EnemyControler enemyControler;
    GameObject target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        GameObject[] magicTowerMovebox = GameObject.FindGameObjectsWithTag("MagicTowerMoveBox");
        Collider2D[] colliders = GetComponents<Collider2D>();

        //Ignorowanie kolizji między określonymi obiektami
        for (int i = 0; i < magicTowerMovebox.Length; i++)
        {
            for (int j = 0; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(magicTowerMovebox[i].GetComponent<Collider2D>(), colliders[j]);
            }
        }   
    }
    void Update()
    {
        EnemyBehaviour();       
    }

    //Zachowanie przeciwnika ChasingEnemy (namierzanie gracza i poruszanie się w jego kierunku)
    void EnemyBehaviour()
    {
        enemyControler = GetComponentInParent<EnemyControler>();
        if(enemyControler.notInRoom == false)
        {
            direction = target.transform.position - transform.position;

            if (direction.x < 0 && facingRight)
            {
                Flip();
            }
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Hitbox")
        {
            target.GetComponent<PlayerControler>().DamagePlayer();
        }
    }

    //Obrócenie animacji poruszania w odpowiednią stronę
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
