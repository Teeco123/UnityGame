using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    public void Talk()
    {
        DialogueManager.Getinstance().EnterDialogueMode(inkJSON);
    }
}
