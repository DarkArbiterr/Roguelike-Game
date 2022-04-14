using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Active,
    Die,
    Waiting
};

public class EnemyControler : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public static EnemyControler instance;
    public EnemyState currentState = EnemyState.Waiting;
    private SpriteRenderer rend;
    public Color colorToTurnTo = Color.red;
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
        Destroy(gameObject);
    }

    IEnumerator HitVisual()
    {
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }

    void Waiting()
    {
        
    }
}
