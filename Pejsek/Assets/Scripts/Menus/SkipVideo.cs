using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipVideo : MonoBehaviour
{
    [SerializeField] Animator blackTransition = default;
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] PauseMenu pauseMenu = default;

    [Header("Buttons")]
    [SerializeField] Animator pauseButtonAnimator = default;
    [SerializeField] Animator skipButtonAnimator = default;
    [SerializeField] float displayDuration = 4;

    [Header("Scene name after video")]
    [SerializeField] string sceneName = "TrainWagon1";

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    bool buttonsVisible = false;

    void Update() {
        // Display control buttons when touch is detect, game is not paused and buttons are not allready shown
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !pauseMenu.IsGamePaused() && !AreButtonsVisible()) {
            pauseButtonAnimator.ResetTrigger(closeTrigger);
            skipButtonAnimator.ResetTrigger(closeTrigger);

            pauseButtonAnimator.SetTrigger(openTrigger);
            skipButtonAnimator.SetTrigger(openTrigger);

            ShowButtons();

            StartCoroutine(ButtonTimer());
        }
    }

    // Skip video
    public void Skip() {
        if (!pauseMenu.IsGamePaused()) {
            StartCoroutine(soundManager.VolumeDown(0.04f));
            StartCoroutine(SkipTransition());
        }
    }

    IEnumerator SkipTransition() {
        blackTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    // Display buttons timer
    IEnumerator ButtonTimer() {
        yield return new WaitForSeconds(displayDuration);

        if (AreButtonsVisible()) {
            pauseButtonAnimator.ResetTrigger(openTrigger);
            skipButtonAnimator.ResetTrigger(openTrigger);
            
            pauseButtonAnimator.SetTrigger(closeTrigger);
            skipButtonAnimator.SetTrigger(closeTrigger);
            HideButtons();

        }
    }

    // Setter show buttons
    void ShowButtons() {
        buttonsVisible = true;
    }

    // Setter hide buttons
    void HideButtons() {
        buttonsVisible = false;
    }

    bool AreButtonsVisible() {
        return buttonsVisible;
    }
    
}
