using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 playerPosition;
    public VectorValue playerStorage;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
