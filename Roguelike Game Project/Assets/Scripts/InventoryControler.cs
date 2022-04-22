using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControler : MonoBehaviour
{
    public int coins = 0;
    public Text coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = coins.ToString("D2");
    }

    public void AddCoin()
    {
        coins++;
    }
}
