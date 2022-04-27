using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasa do obsługi gry

public class GameControler : MonoBehaviour
{
    public static GameControler instance;
    public GameOverScreen gameOverScreen;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    //Wyświetlenie ekranu Game Over
    public void GameOver()
    {
        gameOverScreen.Setup();
    }
}
