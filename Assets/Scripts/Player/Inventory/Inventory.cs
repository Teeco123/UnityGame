using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//Model for inventory
public class InventoryModel
{
    public ScriptableObject item;
    public int quantity;
}

public class Inventory : MonoBehaviour, SavingInterface
{
    public List<InventoryModel> inventory;

    //Adding item to player inventory
    public void AddItem(InventoryModel item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item.item)
            {
                inventory[i].quantity += item.quantity;
                return;
            }
        }
        inventory.Add(item);
    }

    //Saving / Loading data
    public void LoadData(GameData data)
    {
        this.inventory = data.inventory;
    }

    public void SaveData(GameData data)
    {
        data.inventory = this.inventory;
    }
}
