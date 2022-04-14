using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public float lifeTime;
    public float pushPower;
    public float knockTime;
    public float damage;
    private Animator animator;
    public GameObject fallEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Instantiate(fallEffect, transform.position, transform.rotation);
        //Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if(colider.tag == "Enemy")
        {
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
        if(colider.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
