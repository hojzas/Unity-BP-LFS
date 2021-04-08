using System.Collections;
using UnityEngine;

// *****************************************************************************
// Main menu
// *****************************************************************************

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneController sceneController = default;

    [SerializeField] Animator blackTransition = default;
    [SerializeField] Animator aboutPanelAnimator = default;
    [SerializeField] AudioSource backgroundMusic = default;

     [Header("Buttons")]
    [SerializeField] Animator aboutButtonAnimator = default;
    [SerializeField] Animator quitButtonAnimator = default;
    [SerializeField] Animator startButtonAnimator = default;

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    void Start() {
        StartCoroutine(PreloadNextScene());
    }

    IEnumerator PreloadNextScene() {
        // Preload after 1 sec (duration of black transition)
        yield return new WaitForSeconds(1);

        // Use a coroutine to load next Scene in the background
        StartCoroutine(sceneController.LoadAsyncNextScene());
    }

    public void StartGame() {
        StartCoroutine(BackgroundMusicVolumeDown());
        StartCoroutine(LoadGame());
    }

    // Set music volume slowly down
    IEnumerator BackgroundMusicVolumeDown() {

        while (backgroundMusic.volume > 0) {
            backgroundMusic.volume -= 0.01f;
            yield return null;
        }

        backgroundMusic.Stop();
    }

    // Start the game
    IEnumerator LoadGame() {
        startButtonAnimator.SetTrigger("Pressed");
        blackTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        sceneController.LoadPreloadedScene();
    }

    // Display about game panel
    public void AboutGame() {
        aboutPanelAnimator.SetTrigger(openTrigger);
        aboutPanelAnimator.ResetTrigger(closeTrigger);

        aboutButtonAnimator.SetTrigger(closeTrigger);
        aboutButtonAnimator.ResetTrigger(openTrigger);

        quitButtonAnimator.SetTrigger(closeTrigger);
        quitButtonAnimator.ResetTrigger(openTrigger);
    }

    // Close about game panel
    public void CloseAboutGame() {
        aboutPanelAnimator.SetTrigger(closeTrigger);
        aboutPanelAnimator.ResetTrigger(openTrigger);

        aboutButtonAnimator.SetTrigger(openTrigger);
        aboutButtonAnimator.ResetTrigger(closeTrigger);

        quitButtonAnimator.SetTrigger(openTrigger);
        quitButtonAnimator.ResetTrigger(closeTrigger);
    }

    // Quit game
    public void QuitGame() {
        Application.Quit();
    }
}
