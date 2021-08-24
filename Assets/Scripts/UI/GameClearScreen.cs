using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScreen : MonoBehaviour
{
    public void RateGame()
    {
        //direct to app page to rate
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ViPGameStudio.DungeonEscape");
        Debug.Log("Link to Google Play page.");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
