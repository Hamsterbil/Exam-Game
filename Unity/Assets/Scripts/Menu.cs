using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    void OnQuitButton()
    {
        Application.Quit();
    }
    
}