using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ScriptableObject itemData;
    public int quantity;

    public InventoryModel GetItemData()
    {
        return new InventoryModel { item = itemData, quantity = quantity };
    }
}
