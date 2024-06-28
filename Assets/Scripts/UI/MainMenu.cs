using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
        ES3.DeleteFile("SaveFile.es3");
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
