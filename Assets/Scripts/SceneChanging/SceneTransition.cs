using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public bool canEnter;
    public Vector3 playerPosition;
    public VectorValue playerStorage;

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
        if (Input.GetKeyDown(KeyCode.E) && canEnter)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
