using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTowerProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public float lifeTime;
    private GameObject player;
    private Vector2 target;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = new Vector3(player.transform.position.x, player.transform.position.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(
            player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y).normalized * 400;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, projectileSpeed * Time.deltaTime);
        
        
    }
}
