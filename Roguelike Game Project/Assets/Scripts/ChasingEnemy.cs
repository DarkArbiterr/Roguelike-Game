using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public float speed;
    public float attackRange;
    private bool coolDownAttack;
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
            GameControler.DamagePlayer(1); 
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
}
