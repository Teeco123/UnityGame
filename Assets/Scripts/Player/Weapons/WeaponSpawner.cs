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

                //Finding all objects with tag Gun
                GameObject[] gunsInScene;
                gunsInScene = GameObject.FindGameObjectsWithTag("Gun");

                foreach (GameObject gunInScene in gunsInScene)
                {
                    //Retrieving gunstats SO from found gun
                    Shooting gunComponent = gunInScene.GetComponent<Shooting>();

                    if (gunComponent.gunsStats == gunToSpawn)
                    {
                        Debug.Log("Gun Exists");
                        return;
                    }
                    else
                    {
                        //If gun doesn't match with any object on scene gun spawns
                        Debug.Log("Spawning Gun");
                        GameObject spawnedGun = Instantiate(
                            gunToSpawn.prefab,
                            gunToSpawn.position,
                            gunToSpawn.rotation
                        );
                        spawnedGun.transform.SetParent(this.transform, false);
                    }
                }
            }
        }
    }
}
