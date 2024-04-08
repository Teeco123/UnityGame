using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSavingManager : MonoBehaviour
{
    private GameData gameData;

    public static DataSavingManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is Data Saving Manager already!");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame() { }

    public void SaveGame() { }
}
