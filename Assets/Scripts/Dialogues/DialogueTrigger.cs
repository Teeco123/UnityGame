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
        triggerUI.SetActive(false);
    }

    private void CheckNPC()
    {
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
            DialogueNPC npc = hit.transform.GetComponent<DialogueNPC>();
            if (hit.transform.CompareTag("npc"))
            {
                Debug.Log("found npc");
                triggerUI.SetActive(true);
                characterNameTrigger.text = hit.transform.name;
                if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.menuActive)
                {
                    npc.Talk();
                }
            }
            else
            {
                Debug.Log("npc not found");
                triggerUI.SetActive(false);
            }
        }
        else
        {
            Debug.Log("npc not found");
            triggerUI.SetActive(false);
        }
    }
}
