using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler instance;
    public GameOverScreen gameOverScreen;
    
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }

}
