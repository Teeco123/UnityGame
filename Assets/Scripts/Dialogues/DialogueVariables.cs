using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DialogueVariables : MonoBehaviour
{
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("variable changed: " + name + " = " + value);
    }

    public void StartListening(Story story)
    {
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
}
