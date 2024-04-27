using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Trigger UI")]
    [SerializeField]
    private GameObject triggerUI;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    private bool canTalk;

    private void Update()
    {
        if (canTalk)
        {
            triggerUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.menuActive)
            {
                DialogueManager.Getinstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            triggerUI.SetActive(false);
        }
    }

    private void Awake()
    {
        canTalk = false;
        triggerUI.SetActive(false);
    }
}
