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
        //Checking if Data Saving Manager is on the scene
        if (instance != null)
        {
            Debug.LogError("There is Data Saving Manager already!");
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        //Handling Loading when scene in unloaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        //Handling saving when scene in unloaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //Saving game when loading scene
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    //Clearing save file (Not Working 💀💀)
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    //Loading game when loading scene
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.savingInterfaceObjects = FindAllSavingInterfaceObjects();

        //This needs to be here
        LoadGame();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load(); //Passing gamedata from the file

        if (this.gameData == null)
        {
            //Making new game in case no save file is found
            Debug.Log("No data Found");
            NewGame();
        }

        //Loading variables from save file for every script with SavingInterface in it
        foreach (SavingInterface savingInterfaceObj in savingInterfaceObjects)
        {
            savingInterfaceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //Saving variables to save file for every script with SavingInterface in it
        foreach (SavingInterface savingInterfaceObj in savingInterfaceObjects)
        {
            savingInterfaceObj.SaveData(gameData);
        }

        //Passing data to file
        dataHandler.Save(gameData);
    }

    //Saving when leaving game
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //Finding all Saving interface on the scene
    private List<SavingInterface> FindAllSavingInterfaceObjects()
    {
        IEnumerable<SavingInterface> savingInterfaceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<SavingInterface>();

        return new List<SavingInterface>(savingInterfaceObjects);
    }
}
