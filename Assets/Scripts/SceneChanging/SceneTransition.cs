using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public bool canEnter;

    public static bool isEntering { get; private set; }
    public static bool triggeredEnter { get; private set; }
    public Vector3 playerPosition;
    public VectorValue playerStorage;
    public Animator transition;
    public float transitionTime;

    private void Start()
    {
        isEntering = false;
        triggeredEnter = false;
    }

    //Checks if player is near the door
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            canEnter = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            canEnter = false;
    }

    //On input changes scene
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canEnter && !PauseMenu.menuActive)
        {
            triggeredEnter = true;
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        //Plays transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        isEntering = true;

        //Moves player to set scene and set values on door
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
