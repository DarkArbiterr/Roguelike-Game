using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Active,
    Die,
    Waiting
};

public enum EnemyType
{
    MiniSlime,
    BigSlime,
    MagicTower
};

public class EnemyControler : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public static EnemyControler instance;
    public EnemyState currentState = EnemyState.Waiting;
    public EnemyType enemyType;
    public GameObject enemySpawnOnDeath;
    public GameObject coinSpawnOnDeath;
    private SpriteRenderer rend;
    private Color colorToTurnTo = Color.red;
    public bool notInRoom = false;
    public bool isKnockback; 
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case(EnemyState.Waiting):
                Waiting();
                break;
            case(EnemyState.Die):
                Death();
                break;
        }

        if(!notInRoom)
        {
            currentState = EnemyState.Active;
        }
        else
        {
            currentState = EnemyState.Waiting;
        }

        if(health <= 0)
        {
            currentState = EnemyState.Die;
        }
    }

    public void Hit(float value)
    {
        health -= value;
        rend.color = colorToTurnTo;
        StartCoroutine(HitVisual());
    }

    public void Death()
    {
        
        switch (enemyType)
        {
            case(EnemyType.MiniSlime):
                SpawnCoin();
                Destroy(gameObject);
                break;
            case(EnemyType.BigSlime):
                BigSlimeDeath();
                break;
            case(EnemyType.MagicTower):
                SpawnCoin();
                Destroy(gameObject);
                break;
            
        }
    }

    public void SpawnCoin()
    {
        var coin = Instantiate(coinSpawnOnDeath, transform.position,Quaternion.identity);
        coin.transform.parent = gameObject.transform.parent;
    }

    IEnumerator HitVisual()
    {
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }

    void Waiting()
    {
        
    }

    void BigSlimeDeath()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-20f, 20f);
        var newSlime = Instantiate(enemySpawnOnDeath, transform.position + new Vector3(x,y,0), Quaternion.identity);
        newSlime.transform.parent = gameObject.transform.parent;
        x = Random.Range(-20f, 20f);
        y = Random.Range(-20f, 20f);
        newSlime = Instantiate(enemySpawnOnDeath, transform.position + new Vector3(x,y,0), Quaternion.identity);
        newSlime.transform.parent = gameObject.transform.parent;
        x = Random.Range(-20f, 20f);
        y = Random.Range(-20f, 20f);
        newSlime = Instantiate(enemySpawnOnDeath, transform.position + new Vector3(x,y,0), Quaternion.identity);
        newSlime.transform.parent = gameObject.transform.parent;
        x = Random.Range(-20f, 20f);
        y = Random.Range(-20f, 20f);
        newSlime = Instantiate(enemySpawnOnDeath, transform.position + new Vector3(x,y,0), Quaternion.identity);
        newSlime.transform.parent = gameObject.transform.parent;
        Destroy(gameObject);
    }
}
