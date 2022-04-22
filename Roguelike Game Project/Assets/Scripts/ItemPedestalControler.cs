using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestalControler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] itemPrefabs;

    void Start()
    {
        SpawnRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandom()
    {
        var toSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        var spawned = Instantiate(toSpawn, transform.position, Quaternion.identity, this.transform);
        spawned.name = toSpawn.name;
    }
}
