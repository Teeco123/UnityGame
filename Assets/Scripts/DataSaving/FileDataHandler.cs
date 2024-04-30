using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataPath = "";
    private string dataFile = "";

    //Constructor for the class defining datapath and file nam
    public FileDataHandler(string dataPath, string dataFile)
    {
        this.dataPath = dataPath;
        this.dataFile = dataFile;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataPath, dataFile);

        GameData loadedData = null;

        //Checking if save file exists
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                //Reading data from the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad); //Loaded data to variable
            }
            catch (Exception e)
            {
                Debug.LogError("error when loading:" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataPath, dataFile); //Path to save file
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            //Writing game data to save file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error:" + e);
        }
    }
}
