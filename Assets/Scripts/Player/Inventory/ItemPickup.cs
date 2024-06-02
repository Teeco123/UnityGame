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

        //Shooting raycast from camera
        if (
            Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                raycastRange
            )
        )
        {
            //Getting Item script from target hit
            Item item = hit.transform.GetComponent<Item>();

            if (item != null && hit.transform.CompareTag("Item"))
            {
                // Check for Inventory component
                Inventory inventory = GetComponent<Inventory>();
                if (inventory != null)
                {
                    //Adding item to inventory
                    inventory.AddItem(item.GetItemData());

                    //Destroying pickup object if destroyOnPickup is checked
                    if (item.destroyOnPickup == true)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
                else
                {
                    // Handle missing component
                    Debug.LogWarning("Player doesn't have Inventory component");
                }
            }
        }
    }
}
