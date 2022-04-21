using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotionControler : MonoBehaviour
{
    public PlayerControler playerControler;
    // Start is called before the first frame update
    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        playerControler.damage += 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
