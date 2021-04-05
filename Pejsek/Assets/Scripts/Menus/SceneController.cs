using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu = default;
    bool loadScene = false;

    // Pause the game when pressing Android's back button
    void Update()
    {
        if (pauseMenu != null && Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.PauseGame();
        }
    }

    internal void LoadNextScene() {
        loadScene = true;
    }

    // Preload next scene in background
    internal IEnumerator LoadAsyncNextScene() {
        yield return null;

        // Don't let the game scene activate, only preload it
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (loadScene) {
                    // Activate the Scene
                    asyncLoad.allowSceneActivation = true;
                    loadScene = false;
                }
            }
            yield return null;
        }
    }
}
