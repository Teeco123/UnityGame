using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, SavingInterface
{
    [Header("Params")]
    [SerializeField]
    private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField]
    private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField]
    private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private Coroutine DisplayLineCoroutine;

    private bool canSkip = false;

    private bool submitSkip;

    static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";

    private DialogueVariables dialogueVariables;

    //Makes sure there is only one Dialogue Manager in the scene
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one dialogue Manager in the scene");
        }
        instance = this;
    }

    //Returns singleton instance of manager
    public static DialogueManager Getinstance()
    {
        return instance;
    }

    //Initializes default states for UI and choices
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //Skipping dialogue
        if (Input.GetKeyDown(KeyCode.Return))
        {
            submitSkip = true;
        }

        if (!dialogueIsPlaying)
        {
            return;
        }

        //Moving to next dialogue after button input
        if (canContinueToNextLine && Input.GetKeyDown(KeyCode.Return))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //Loading dialogue from file
        currentStory = new Story(inkJSON.text);

        //Activating dialogue UI
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        displayNameText.text = "???";

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        //Disabling UI
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    //Moving dialogue to next line
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //If dialogue typing is over
            if (DisplayLineCoroutine != null)
            {
                StopCoroutine(DisplayLineCoroutine);
            }
            //Start typing new line
            DisplayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            //if can't continue exit dialogue mode
            ExitDialogueMode();
        }
    }

    //Makes that player can skip typing dialogue after given value
    private IEnumerator CanSkip()
    {
        canSkip = false; //Making sure the variable is false.
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

    //Typing effect for dialogues
    private IEnumerator DisplayLine(string line)
    {
        //Defaults dialogue system
        dialogueText.text = "";
        submitSkip = false;
        canContinueToNextLine = false;
        HideChoices();

        StartCoroutine(CanSkip());

        //Looping through each letter from dialogue
        foreach (char letter in line.ToCharArray())
        {
            if (canSkip && submitSkip)
            {
                //Skips typing animation
                submitSkip = false;
                dialogueText.text = line;
                break;
            }
            //Displays next character after given value (typingSpeed)
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //Typing is complete
        canContinueToNextLine = true;
        canSkip = false;
        DisplayChoices();
    }

    //Processing tags from inky dialogues
    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            //Splitting tags
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag is not working");
            }
            //Extracting values from tags
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Handling tags
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                default:
                    Debug.LogWarning("tag is not used now");
                    break;
            }
        }
    }

    //Hiding choices buttons from UI
    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        //Retrieving choices from ink file
        List<Choice> currentChoices = currentStory.currentChoices;

        //Checking if UI supports that many choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("more choices than ui buttons");
        }
        int index = 0;

        //Turning on choices and settings names for each choice from file
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //Hiding unused buttons
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    //Selects default first choice
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    //Handles selecting choice
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }

    //Loading dialogue variables
    public void LoadData(GameData data)
    {
        // now we can create a new DialogueVariables object that's being initialized based on any loaded data
        dialogueVariables = new DialogueVariables(loadGlobalsJSON, data.currentVariables);
    }

    //Saving dialogue variables
    public void SaveData(GameData data)
    {
        // when we save the game, we get the current global state from our dialogue variables and then save that to our data
        string globalStateJson = dialogueVariables.GetGlobalVariablesStateJson();
        data.currentVariables = globalStateJson;
    }
}
