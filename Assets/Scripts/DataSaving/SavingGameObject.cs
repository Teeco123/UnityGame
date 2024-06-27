using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingGameObject : MonoBehaviour
{
    public string guid = System.Guid.NewGuid().ToString();

    void Awake()
    {
        if (ES3.KeyExists("GameObject: " + guid))
            Destroy(this.gameObject);
    }

    void OnApplicationQuit()
    {
        ES3.Save<bool>("GameObject: " + guid, true);
    }
}
