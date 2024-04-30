using System.Collections;
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
        //Add controller to player object
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Stops moving player when dialogue is playing
        if (DialogueManager.Getinstance().dialogueIsPlaying || SceneTransition.triggeredEnter)
        {
            return;
        }

        //Checks if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Makes sure player doesn't gain velocity when is grounded //FIX: player starts falling slowly when near ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Gets player input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Moves player
        controller.Move(move * speed * Time.deltaTime);

        //Handles Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Handles Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        //Saving on F5
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

    //Moves player to door location after moving to saved Vector3
    IEnumerator DelayThenTransform(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startingPosition.initialValue;
    }
}
