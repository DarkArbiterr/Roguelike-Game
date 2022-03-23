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

        //move.x = Input.GetAxisRaw("Horizontal");
        //move.y = Input.GetAxisRaw("Vertical");

        move = new Vector2(0f, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            move.y = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.y = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x = +1f;
        }

        moveDirection = new Vector3(move.x, move.y).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMovement()
    {
        Vector2 movement = new Vector2(0f, 0f);
        bool isIdle;

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = +1f;
        }

        isIdle = movement.x == 0 && movement.y == 0;

        if (isIdle)
        {
            // TODO - animacja "player idle"
        }
        else
        {
            Vector3 moveDirection = new Vector3(movement.x, movement.y).normalized;
            Vector3 targetMovePosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDirection, moveSpeed * Time.deltaTime, LayerMask.GetMask("Blocking"));
            if (raycastHit.collider == null)
            {
                // Można się ruszyć, nie napotkano przeszkody
                // TODO - lastMoveDirection do animacji "player idle"
                // TODO - animacja chodzenia
                transform.position = targetMovePosition;
            }
            else
            {
                // Nie można się ruszyć Diagonalnie, napotkano przeszkodę
                Vector3 testMoveDirection = new Vector3(moveDirection.x, 0f).normalized;
                targetMovePosition = transform.position + testMoveDirection * moveSpeed * Time.deltaTime;
                raycastHit = Physics2D.Raycast(transform.position, testMoveDirection, moveSpeed * Time.deltaTime, LayerMask.GetMask("Blocking"));
                if (testMoveDirection.x != 0f && raycastHit.collider == null)
                {
                    // Można się ruszyć Horyzontalnie
                    // TODO - lastMoveDirection do animacji "player idle"
                    // TODO - animacja chodzenia
                    transform.position = targetMovePosition;
                }
                else
                {
                    // Nie można ruszyć się Horyzontalnie
                    testMoveDirection = new Vector3(0f, moveDirection.y).normalized;
                    targetMovePosition = transform.position + testMoveDirection * moveSpeed * Time.deltaTime;
                    raycastHit = Physics2D.Raycast(transform.position, testMoveDirection, moveSpeed * Time.deltaTime, LayerMask.GetMask("Blocking"));
                    if (testMoveDirection.y != 0f && raycastHit.collider == null)
                    {
                        // Można ruszyć się Wertykalnie
                        // TODO - lastMoveDirection do animacji "player idle"
                        // TODO - animacja chodzenia
                        transform.position = targetMovePosition;
                    }
                    else
                    {
                        // Nie można ruszyć się Wertykalnie
                        // Animacja player idle
                    }
                }
            }
        }

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
    }
}
