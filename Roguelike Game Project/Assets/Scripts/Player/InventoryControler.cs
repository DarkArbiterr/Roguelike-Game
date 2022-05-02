using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasa obsługująca inwentarz gracza

public class InventoryControler : MonoBehaviour
{
    public int coins = 0;
    public Text coinCounter;

    void Start()
    {
        
    }

    void Update()
    {
        coinCounter.text = coins.ToString("D2");
    }

    //Dodanie monety do wyposażenia 
    public void AddCoin()
    {
        coins++;
    }
}
