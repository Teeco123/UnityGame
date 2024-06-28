using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingDestroyed : MonoBehaviour
{
    bool isApplicationQuitting = false;
    string uId;

    void Start()
    {
        IDGenerator idGenerator = gameObject.GetComponent<IDGenerator>();
        uId = idGenerator.guid;

        if (ES3.KeyExists("GOState: " + uId))
            Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if (gameObject.scene.isLoaded && !isApplicationQuitting)
            ES3.Save<bool>("GOState: " + uId, true);
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}
