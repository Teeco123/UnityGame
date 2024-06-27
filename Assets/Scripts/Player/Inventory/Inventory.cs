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

public class Inventory : MonoBehaviour
{
    public List<InventoryModel> inventory;

    void OnApplicationQuit()
    {
        ES3.Save("inventory", inventory);
    }

    void Awake()
    {
        inventory = ES3.Load("inventory", new List<InventoryModel>());
    }

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
}
