using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Camera playerCamera;
    public float raycastRange = 5;

    private void Start() { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
        }
    }

    void CheckItem()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                raycastRange
            )
        )
        {
            Item item = hit.transform.GetComponent<Item>();
            if (item != null && hit.transform.CompareTag("Item"))
            {
                Inventory inventory = GetComponent<Inventory>(); // Check for Inventory component
                if (inventory != null)
                {
                    inventory.AddItem(item.GetItemData());
                }
                else
                {
                    Debug.LogWarning("Player doesn't have Inventory component!"); // Handle missing component
                }
            }
        }
    }
}
