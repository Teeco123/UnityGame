using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingGameObject : MonoBehaviour
{
    public string guid = System.Guid.NewGuid().ToString();
    static bool isApplicationQuitting = false;

    void Start()
    {
        if (ES3.KeyExists("GameObject: " + guid))
            Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if (gameObject.scene.isLoaded && !isApplicationQuitting)
            ES3.Save<bool>("GameObject: " + guid, true);
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}
