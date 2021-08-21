using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(_currentScene + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
