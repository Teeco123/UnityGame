using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
