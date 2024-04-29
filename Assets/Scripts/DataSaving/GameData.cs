using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public string currentVariables;
    public List<InventoryModel> inventory;

    public GameData()
    {
        playerPosition = new Vector3(0, 1, 0);
        currentVariables = "";
        inventory = new List<InventoryModel>();
    }
}
