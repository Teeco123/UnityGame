using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        File.Delete(Application.persistentDataPath + "/SaveFile");
        DataSavingManager.instance.NewGame();

        SceneManager.LoadSceneAsync("Scene1");
    }

    public void BackMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
        DataSavingManager.instance.SaveGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
