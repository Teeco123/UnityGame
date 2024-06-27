using System.Collections;
using ES3Types;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    void OnApplicationQuit()
    {
        ES3.Save("playerPosition", transform.position);
    }

    void Awake()
    {
        transform.position = ES3.Load("myPosition", Vector3.zero);
    }

    void Start()
    {
        //Add controller to player object
        controller = gameObject.GetComponent<CharacterController>();
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
}
