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

                //Looping through all guns in scene
                //TODO: This part of code don't work correctly

                //wojsen's change:
                bool foundItemIsInScene = false;


                foreach (GameObject gunInScene in gunsInScene)
                {
                    //Retrieving Shooting component from gun
                    Shooting gunComponent = gunInScene.GetComponent<Shooting>();

                    Debug.Log("Gun to Spawn: " + gunToSpawn.gunName);
                    Debug.Log("Gun in Scene: " + gunComponent.gunsStats.gunName);

                    //Comparing gun in scene with gun that gonna spawn
                    //If gun exists on scene gun doesn't spawn
                    if (gunComponent.gunsStats.gunName == gunToSpawn.gunName)
                    {
                        Debug.Log("Gun Exists");
                        foundItemIsInScene = true;
                        break;
                    }
                    
                }
                //If gun doesn't match with any object on scene gun spawns
                if (foundItemIsInScene == false)
                {
                    
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
