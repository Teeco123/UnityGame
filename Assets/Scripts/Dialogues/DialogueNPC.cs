using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that attaches to NPC where u can set dialogue file
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
