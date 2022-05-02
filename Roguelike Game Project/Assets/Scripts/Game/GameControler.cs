using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasa do obsługi gry

public class GameControler : MonoBehaviour
{
    public static GameControler instance;
    public GameOverScreen gameOverScreen;
    private InventoryControler inventory;
    public ItemInfoScreen itemInfoScreen;
    public WinScreen winScreen;
    private GameObject[] totalEnemies;
    public int totalCoins;
    
    void Start()
    {
        instance = this;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryControler>();
    }
    
    void Update()
    {
        if(inventory.coins == totalCoins && totalCoins != 0)
            Win(totalCoins);
    }

    public void CountEnemies()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        CountCoins();
        Debug.Log(totalEnemies.Length);
    }

    public void CountCoins()
    {
        totalCoins = totalEnemies.Length;
        foreach (GameObject enemy in totalEnemies)
        {
            switch(enemy.GetComponent<EnemyControler>().enemyType)
            {
                case(EnemyType.BigSlime):
                    totalCoins += 3;
                    break;
            }
        }
        Debug.Log(totalCoins);
    }

    public void SetupInfo(string title, string descr)
    {
        itemInfoScreen.Setup(title, descr);
    }

    //Wyświetlenie ekranu Game Over
    public void GameOver(int coins)
    {
        gameOverScreen.Setup(coins);
    }

    public void Win(int coins)
    {
        winScreen.Setup(coins);
    }
}
