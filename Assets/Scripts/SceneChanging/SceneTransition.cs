using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public bool canEnter;

    public static bool isEntering { get; private set; }
    public Vector3 playerPosition;
    public VectorValue playerStorage;
    public Animator transition;
    public float transitionTime;

    private void Start()
    {
        isEntering = false;
    }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canEnter && !PauseMenu.menuActive)
        {
            StartCoroutine(LoadLevel());
        }
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        isEntering = true;
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
