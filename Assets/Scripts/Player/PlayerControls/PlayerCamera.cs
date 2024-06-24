using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xRotation = 0f;

    public Transform body;
    public float mouseSensitivity = 100f;

    void Start()
    {
        //Locks cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Stops moving camera when dialogue is playing
        if (DialogueManager.Getinstance().dialogueIsPlaying || SceneTransition.triggeredEnter)
        {
            return;
        }

        //Gets player input from mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Locks player from moving camera more than 90 degrees up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotates camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
