using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public float lifeTime;
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
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if(colider.tag == "Enemy")
        {
            colider.gameObject.GetComponent<EnemyControler>().Death();
            Destroy(gameObject);
        }
        if(colider.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
