using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, SavingInterface
{
    private CharacterController controller;
    private Vector3 velocity;
    private float speed = 2.0f;
    private float gravity = -9.81f;

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
            speed = 4.0f;
        }
        else
        {
            speed = 2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

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
