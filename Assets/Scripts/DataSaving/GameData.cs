using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //All variables for saving
    public Vector3 playerPosition;
    public string currentVariables;
    public List<InventoryModel> inventory;

    //Default data for variables
    public GameData()
    {
        playerPosition = new Vector3(0, 1, 0);
        currentVariables = "";
        inventory = new List<InventoryModel>();
    }
}
