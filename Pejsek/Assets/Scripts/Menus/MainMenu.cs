using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// *****************************************************************************
// Main menu
// *****************************************************************************

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator blackTransition = default;
    [SerializeField] Animator aboutPanelAnimator = default;
    [SerializeField] Animator aboutButtonAnimator = default;
    [SerializeField] Animator quitButtonAnimator = default;
    [SerializeField] Animator startButtonAnimator = default;

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    public void StartGame() {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame() {
        startButtonAnimator.SetTrigger("Pressed");
        blackTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AboutGame() {
        aboutPanelAnimator.SetTrigger(openTrigger);
        aboutPanelAnimator.ResetTrigger(closeTrigger);

        aboutButtonAnimator.SetTrigger(closeTrigger);
        aboutButtonAnimator.ResetTrigger(openTrigger);

        quitButtonAnimator.SetTrigger(closeTrigger);
        quitButtonAnimator.ResetTrigger(openTrigger);
    }

    public void CloseAboutGame() {
        aboutPanelAnimator.SetTrigger(closeTrigger);
        aboutPanelAnimator.ResetTrigger(openTrigger);

        aboutButtonAnimator.SetTrigger(openTrigger);
        aboutButtonAnimator.ResetTrigger(closeTrigger);

        quitButtonAnimator.SetTrigger(openTrigger);
        quitButtonAnimator.ResetTrigger(closeTrigger);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
