using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;

    public Transform head;
    public Transform body;
    public float mouseSensitivity = 100f;

    void Start()
    {
        //Locks cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Gets player input from mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Stops moving camera when dialogue is playing
        if (DialogueManager.Getinstance().dialogueIsPlaying || SceneTransition.triggeredEnter)
        {
            return;
        }

        //Locks player from moving camera more than 90 degrees up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 75f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);

        //Rotates camera
        head.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
