using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Klasa obsługująca menu startowe

public class StartMenuControler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Przycisk startu
    public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Przycisk wyjścia
    public void QuitButton()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
