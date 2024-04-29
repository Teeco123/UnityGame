using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ScriptableObject itemData;
    public int quantity;

    //Class to reference in other scripts so u can get item data
    public InventoryModel GetItemData()
    {
        return new InventoryModel { item = itemData, quantity = quantity };
    }
}
