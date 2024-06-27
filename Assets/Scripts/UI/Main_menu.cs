using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void BackMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
