using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Exit : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());

        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime (delay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene+1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScene();
        SceneManager.LoadScene(nextSceneIndex);
    }

}
