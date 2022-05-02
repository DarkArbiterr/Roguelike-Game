using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiedzialna za obsługę gracza

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
    public float invincibleTime;
    public float invincibleDeltaTime;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private float shootHorizontal, shootVertical;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public Vector2 move;
    private Vector3 moveDirection;
    private Animator animator;
    private HealthControler healthControler;
    private InventoryControler inventoryControler;
    private bool isInvincible = false;
    public float damage = 5;
    public GameObject model;
    private GameControler gameController;
    
    void Update()
    {
        //Poruszanie się i ustalanie kierunku strzału
        shootHorizontal = Input.GetAxis("ShootHorizontal");
        shootVertical = Input.GetAxis("ShootVertical");

        if((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(move.x, move.y).normalized;
        model.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveDirection.magnitude * moveSpeed));

        bool flipped = moveDirection.x < 0;
        model.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void FixedUpdate()
    {
            rb.velocity = moveDirection * moveSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthControler = GetComponent<HealthControler>();
        inventoryControler = GetComponent<InventoryControler>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
    }

    //Otrzymywanie obrażeń
    public void DamagePlayer()
    {
        if(isInvincible)
            return;
        
        healthControler.health -= 1;
        
        if (healthControler.health == 0)
        {
            KillPlayer();
            return;
        }

        StartCoroutine(Invulnerable());
    }

    //Czas przez jaki gracz jest niewrażliwy na ataki (i wizualizacja tego stanu)
    IEnumerator Invulnerable()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (float i = 0; i < invincibleTime; i += invincibleDeltaTime)
        {
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }
        isInvincible = false;
        ScaleModelTo(Vector3.one);
        Debug.Log("Player is no longer invincible!");
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }

    //Zabicie gracza
    public void KillPlayer()
    {
        healthControler.hearts[0].sprite = healthControler.emptyHeart;
        gameController.GameOver(inventoryControler.coins);
        Destroy(gameObject);
    }

    //Wystrzelenie strzały
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
        bullet.GetComponent<BulletControler>().damage = damage;
        // strzał w prawo
        if (x > 0 && y == 0)
            bullet.transform.rotation = Quaternion.Euler(0,0,0);
        // strzał w górę
        if (x == 0 && y > 0)
            bullet.transform.rotation = Quaternion.Euler(0,0,90);
        // strzał w górę i w prawo
        if (x > 0 && y > 0)
            bullet.transform.rotation = Quaternion.Euler(0,0,45);
        // strzał w dół i w prawo
        if (x > 0 && y < 0)
            bullet.transform.rotation = Quaternion.Euler(0,180,-135);
        // strzał w dół
        if (x == 0 && y < 0)
            bullet.transform.rotation = Quaternion.Euler(0,0,-90);
        // strzał w dół i lewo 
        if (x < 0 && y < 0)
            bullet.transform.rotation = Quaternion.Euler(0,0,-135);
        // strzał w lewo 
        if (x < 0 && y == 0)
            bullet.transform.rotation = Quaternion.Euler(180,0,180);
        // strzał w lewo i w górę 
        if (x < 0 && y > 0)
            bullet.transform.rotation = Quaternion.Euler(0,180,45);
    }
}
