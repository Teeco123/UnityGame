using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataSavingManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField]
    private string fileName;
    private GameData gameData;
    private List<SavingInterface> savingInterfaceObjects;
    private FileDataHandler dataHandler;

    public static DataSavingManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is Data Saving Manager already!");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.savingInterfaceObjects = FindAllSavingInterfaceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data Found");
            NewGame();
        }

        foreach (SavingInterface savingInterfaceObj in savingInterfaceObjects)
        {
            savingInterfaceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (SavingInterface savingInterfaceObj in savingInterfaceObjects)
        {
            savingInterfaceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<SavingInterface> FindAllSavingInterfaceObjects()
    {
        IEnumerable<SavingInterface> savingInterfaceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<SavingInterface>();

        return new List<SavingInterface>(savingInterfaceObjects);
    }
}
