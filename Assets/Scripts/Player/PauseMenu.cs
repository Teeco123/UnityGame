using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject target, options, volume, resolution, main;

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

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0f;

                target.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        menuActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

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