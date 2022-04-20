using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float attackRange;
    private bool coolDownAttack;
    private bool facingRight = true;
    public float coolDown;
    public EnemyControler enemyControler;
    GameObject target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
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

            if(Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                Attack();
            else
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
            
    }

    void Attack()
    {
        if(!coolDownAttack)
        {
            target.GetComponent<PlayerControler>().DamagePlayer();
            StartCoroutine(CoolDown());
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
}
