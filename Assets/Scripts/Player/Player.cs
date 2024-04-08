using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayer();
        }
    }
}
