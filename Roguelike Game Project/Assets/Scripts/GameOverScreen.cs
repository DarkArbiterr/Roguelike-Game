using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Klasa do obsługi ekranu Game Over

public class GameOverScreen : MonoBehaviour
{
    //Wyświetlenie ekranu Game Over
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    //Obsługa przycisku Restart
    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Obsługa przycisku Back to Menu
    public void StartMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
