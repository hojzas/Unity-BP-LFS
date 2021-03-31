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
        // If detect touch, game is not paused and buttons are not allready shown
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !pauseMenu.paused && !buttonsVisible) {
            pauseButtonAnimator.ResetTrigger(closeTrigger);
            skipButtonAnimator.ResetTrigger(closeTrigger);

            pauseButtonAnimator.SetTrigger(openTrigger);
            skipButtonAnimator.SetTrigger(openTrigger);

            buttonsVisible = true;

            StartCoroutine(ButtonTimer());
        }
    }

    public void Skip() {
        if (!pauseMenu.paused) {
            StartCoroutine(soundManager.VolumeDown(0.05f));
            StartCoroutine(SkipTransition());
        }
    }

    IEnumerator SkipTransition() {
        blackTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ButtonTimer() {
        yield return new WaitForSeconds(displayDuration);

        if (buttonsVisible) {
            pauseButtonAnimator.ResetTrigger(openTrigger);
            skipButtonAnimator.ResetTrigger(openTrigger);
            
            pauseButtonAnimator.SetTrigger(closeTrigger);
            skipButtonAnimator.SetTrigger(closeTrigger);
            buttonsVisible = false;

        }
    }

    
}
