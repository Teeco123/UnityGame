using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync("Scene1");
    }

    public void NewGame()
    {
        DataSavingManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Scene1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
