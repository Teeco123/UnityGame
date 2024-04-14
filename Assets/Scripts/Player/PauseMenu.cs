using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject target;

    public static bool menuActive { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (target.activeSelf)
            {
                Resume();
            }
            else
            {
                menuActive = true;
                Time.timeScale = 0f;
                target.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        menuActive = false;
        Time.timeScale = 1f;
        target.SetActive(false);
    }

    private void Start()
    {
        Resume();
    }
}
