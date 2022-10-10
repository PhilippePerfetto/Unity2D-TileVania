using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1.0f;

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player")
            StartCoroutine(LoadingNextLevel());
    }

    IEnumerator LoadingNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = levelIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
