using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator blackTransitionAnimator = default;

    bool loadScene = false;

    // Setter
    internal void LoadPreloadedScene() {
        loadScene = true;
    }

    // Preload next scene in background
    internal IEnumerator LoadAsyncNextScene() {
        yield return null;

        // Don't let the game scene activate, only preload it
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            if (asyncLoad.progress >= 0.9f) {
                if (loadScene) {
                    // Activate the Scene
                    asyncLoad.allowSceneActivation = true;
                    loadScene = false;
                }
            }
            yield return null;
        }
    }

    // Load next scene
    // Load with black transition if required
    internal IEnumerator LoadNextScene(bool loadWithBlackTransition) {
        if (loadWithBlackTransition) {
            blackTransitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Load scene
    internal IEnumerator LoadSceneByName(string name) {
        blackTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
    }

    // Load Main menu scene
    internal IEnumerator LoadMainMenuScene(bool loadWithBlackTransition) {
        if (loadWithBlackTransition) {
            blackTransitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(0);
    }
}
