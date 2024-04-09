using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueVariables : MonoBehaviour
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    private Story globalVariablesStory;

    public DialogueVariables(TextAsset loadGlobalsJSON, string globalStateJson)
    {
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        if (!globalStateJson.Equals(""))
        {
            globalVariablesStory.state.LoadJson(globalStateJson);
        }

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(
                name
            );
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public string GetGlobalVariablesStateJson()
    {
        VariablesToStory(globalVariablesStory);
        return globalVariablesStory.state.ToJson();
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
}
