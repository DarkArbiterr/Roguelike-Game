using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public Text coinsText;

    //Wyświetlenie ekranu Win
    public void Setup(int coins)
    {
        coinsText.text = "Coins: " + coins.ToString("D2");
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
