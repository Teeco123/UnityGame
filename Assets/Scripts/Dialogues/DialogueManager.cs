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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager Getinstance()
    {
        return instance;
    }

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            submitSkip = true;
        }
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine && Input.GetKeyDown(KeyCode.Return))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        displayNameText.text = "???";

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (DisplayLineCoroutine != null)
            {
                StopCoroutine(DisplayLineCoroutine);
            }
            DisplayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator CanSkip()
    {
        canSkip = false; //Making sure the variable is false.
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        submitSkip = false;
        canContinueToNextLine = false;
        HideChoices();

        StartCoroutine(CanSkip());

        foreach (char letter in line.ToCharArray())
        {
            if (canSkip && submitSkip)
            {
                submitSkip = false;
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueToNextLine = true;
        canSkip = false;
        DisplayChoices();
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag is not working");
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

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

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("more choices than ui buttons");
        }
        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }

    public void LoadData(GameData data)
    {
        // now we can create a new DialogueVariables object that's being initialized based on any loaded data
        dialogueVariables = new DialogueVariables(loadGlobalsJSON, data.currentVariables);
    }

    public void SaveData(GameData data)
    {
        // when we save the game, we get the current global state from our dialogue variables and then save that to our data
        string globalStateJson = dialogueVariables.GetGlobalVariablesStateJson();
        data.currentVariables = globalStateJson;
    }
}
