using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa obsługująca piedestał na przedmiot

public class ItemPedestalControler : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    void Start()
    {
        SpawnRandom();
    }

    void Update()
    {
        
    }

    //Wybranie losowego przedmiotu z określonego zbioru
    private void SpawnRandom()
    {
        var toSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        var spawned = Instantiate(toSpawn, transform.position, Quaternion.identity, this.transform);
        spawned.name = toSpawn.name;
    }
}
