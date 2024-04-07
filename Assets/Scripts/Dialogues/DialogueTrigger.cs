using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Trigger Cue")]
    [SerializeField]
    private GameObject triggerCue;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange)
        {
            triggerCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Getinstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            triggerCue.SetActive(false);
        }
    }

    private void Awake()
    {
        playerInRange = false;
        triggerCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
