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
    public static EnemyControler instance;
    public EnemyState currentState = EnemyState.Waiting;
    public bool notInRoom = false;
    void Start()
    {
        
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
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void Waiting()
    {
        
    }
}
