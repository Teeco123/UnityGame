using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, SavingInterface
{
    private CharacterController controller;
    private Vector3 velocity;
    private float speed;
    public float walkSpeed = 2f;
    public float sprintSpeed = 4f;
    public float gravity = -9.81f;
    public float jump = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

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
        if (DialogueManager.Getinstance().dialogueIsPlaying || SceneTransition.triggeredEnter)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

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
