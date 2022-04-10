using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
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
    
    // Update is called once per frame
    void Update()
    {
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
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.magnitude * moveSpeed));

        bool flipped = moveDirection.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            var movement = moveDirection * moveSpeed * Time.deltaTime;
            this.transform.Translate(movement, Space.World);
            //rb.velocity = moveDirection * moveSpeed;
        }
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );

        // strzał w prawo
        if (x > 0 && y == 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,0);
        }
        // strzał w górę
        if (x == 0 && y > 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,90);
        }
        // strzał w górę i w prawo
        if (x > 0 && y > 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,45);
        }
        // strzał w dół i w prawo
        if (x > 0 && y < 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,-45);
        }
        // strzał w dół
        if (x == 0 && y < 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,-90);
        }
        // strzał w dół i lewo
        if (x < 0 && y < 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,-135);
        }
        // strzał w lewo
        if (x < 0 && y == 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,180);
        }
        // strzał w lewo i w górę
        if (x < 0 && y > 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,135);
        }
    }
}
