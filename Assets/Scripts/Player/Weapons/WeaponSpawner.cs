using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject playerObject;

    public Guns[] gunList;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckGuns();
        }
    }

    void CheckGuns()
    {
        List<InventoryModel> foundItems;
        InventoryModel[] foundItemsArray;
        Guns gunToSpawn;
        //Referencing Inventory script on Player object
        Inventory inventory = playerObject.GetComponent<Inventory>();

        foreach (Guns gun in gunList)
        {
            //Finding all guns in inventory based on GunList and converting this to array
            foundItems = inventory.inventory.FindAll(item => item.item == gun);
            foundItemsArray = foundItems.ToArray();

            foreach (InventoryModel foundItem in foundItemsArray)
            {
                //Converting item from InventoryModel to Guns class
                gunToSpawn = (Guns)foundItem.item;

                //Spawning weapon in weapon holder based on weapons in inventory
                GameObject spawnedGun = Instantiate(
                    gunToSpawn.prefab,
                    gunToSpawn.position,
                    gunToSpawn.rotation
                );
                spawnedGun.SetActive(false);
                spawnedGun.transform.SetParent(this.transform, false);
            }
        }
    }
}
