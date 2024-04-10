using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.savingInterfaceObjects = FindAllSavingInterfaceObjects();

        //This needs to be here
        LoadGame();
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
            savingInterfaceObj.SaveData(gameData);
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
