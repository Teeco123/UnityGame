using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Trigger UI")]
    [SerializeField]
    private GameObject triggerUI;

    [SerializeField]
    private TextMeshProUGUI characterNameTrigger;

    [Header("Raycasts")]
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float raycastRange;

    private void Update()
    {
        CheckNPC();
    }

    private void Awake()
    {
        //Hides UI at start
        triggerUI.SetActive(false);
    }

    private void CheckNPC()
    {
        //Shoots raycast from camera
        RaycastHit hit;
        if (
            Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                raycastRange
            )
        )
        {
            //Gets DialogueNPC component from target
            DialogueNPC npc = hit.transform.GetComponent<DialogueNPC>();

            if (hit.transform.CompareTag("npc"))
            {
                //Activates UI with name of target object
                triggerUI.SetActive(true);
                characterNameTrigger.text = hit.transform.name;
                if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.menuActive)
                {
                    npc.Talk();
                }
            }
            else
            {
                triggerUI.SetActive(false);
            }
        }
        else
        {
            triggerUI.SetActive(false);
        }
    }
}
