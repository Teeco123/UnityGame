using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && target.activeSelf)
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            target.SetActive(true);
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        target.SetActive(false);
    }
}