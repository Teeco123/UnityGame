using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, SavingInterface
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    public VectorValue startingPosition;

    private void OnEnable()
    {
        Delay();
    }

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frameAW
    void Update()
    {
        if (DialogueManager.Getinstance().dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 4.0f;
        }
        else
        {
            playerSpeed = 2.0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F5))
        {
            DataSavingManager.instance.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    private void Delay()
    {
        if (SceneTransition.isEntering)
        {
            StartCoroutine(DelayThenTransform(0.1f));
        }
    }

    IEnumerator DelayThenTransform(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startingPosition.initialValue;
    }
}
