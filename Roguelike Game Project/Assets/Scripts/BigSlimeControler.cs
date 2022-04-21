using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeControler : MonoBehaviour
{
    private GameObject player;
    private bool coolDownAttack;
    public float coolDown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Hitbox" && !coolDownAttack)
        {
            player.GetComponent<PlayerControler>().DamagePlayer();
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
