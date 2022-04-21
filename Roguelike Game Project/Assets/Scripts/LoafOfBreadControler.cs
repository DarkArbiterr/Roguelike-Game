using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoafOfBreadControler : MonoBehaviour
{
    public HealthControler healthControler;
    // Start is called before the first frame update
    void Start()
    {
        healthControler = GetComponent<HealthControler>();
        healthControler.numberOfHearts += 2;
        healthControler.health = healthControler.numberOfHearts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
