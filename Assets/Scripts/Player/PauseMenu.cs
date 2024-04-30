using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject target,
        options,
        volume,
        resolution,
        main;

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

                //Shows cursor on screen
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                //Freezes the game
                Time.timeScale = 0f;

                //Shows UI
                target.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        menuActive = false;

        //Locks and hides cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Unfreezes game
        Time.timeScale = 1f;

        //Turns off UI
        target.SetActive(false);
        options.SetActive(false);
        volume.SetActive(false);
        resolution.SetActive(false);
        main.SetActive(true);
    }

    private void Start()
    {
        Resume();
    }
}
