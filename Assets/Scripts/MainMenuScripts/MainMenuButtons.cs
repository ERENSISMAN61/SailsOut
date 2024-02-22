using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void playGameStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void creditsScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void gameQuit()
    {
        Application.Quit();
    }

}