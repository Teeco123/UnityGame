using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DialogueVariables : MonoBehaviour
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    private Story globalVariablesStory;

    public DialogueVariables(TextAsset loadGlobalsJSON, string globalStateJson)
    {
        //Gets variables from globals variables file
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        if (!globalStateJson.Equals(""))
        {
            //Loading saved variables to story object
            globalVariablesStory.state.LoadJson(globalStateJson);
        }

        variables = new Dictionary<string, Ink.Runtime.Object>();

        //Adding variables from story object to Dictionary
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(
                name
            );
            variables.Add(name, value);
        }
    }

    //Returns state of all variables as JSON
    public string GetGlobalVariablesStateJson()
    {
        VariablesToStory(globalVariablesStory);
        return globalVariablesStory.state.ToJson();
    }

    //Called when variable is changed
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //Checking if variable exists
        if (variables.ContainsKey(name))
        {
            //Overwrites variable value
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    //Synchronizing variables between dictionary and story object
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    //Start listening for variables changes
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    //Stops listening for variable changes
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
}
