using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryModel
{
    public ScriptableObject item;
    public int quantity;
}

public class Inventory : MonoBehaviour, SavingInterface
{
    public List<InventoryModel> inventory;

    public void LoadData(GameData data)
    {
        this.inventory = data.inventory;
    }

    public void SaveData(GameData data)
    {
        data.inventory = this.inventory;
    }
}
