using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// *****************************************************************************
// Pause menu
// *****************************************************************************

public class PauseMenu : MonoBehaviour
{
    [Header("Pause menu settings")]
    [SerializeField] Button pauseButton = default;
    [SerializeField] GameObject aboutPanel = default;
    [SerializeField] Animator pauseMenuAnimator = default;

    [SerializeField] Animator blackTransition = default;

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    Animator pauseButtonAnimator;
    private AudioSource[] audioSources;
    internal bool paused = false;

    void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);

        pauseButtonAnimator = pauseButton.gameObject.GetComponent<Animator>();
    }

    // Pause
    void PauseGame() {
        Time.timeScale = 0f;

        PauseAudio(true);

        // Open pause menu
        pauseMenuAnimator.SetTrigger(openTrigger);
        pauseMenuAnimator.ResetTrigger(closeTrigger);
        paused = true;
        // Close pause button
        pauseButtonAnimator.SetTrigger(closeTrigger);
        pauseButtonAnimator.ResetTrigger(openTrigger);
    }

    // Resume
    public void ResumeGame() {
        Time.timeScale = 1f;

        PauseAudio(false);

        pauseMenuAnimator.SetTrigger(closeTrigger);
        pauseMenuAnimator.ResetTrigger(openTrigger);
        StartCoroutine(WaitForAnimation());
        pauseButtonAnimator.SetTrigger(openTrigger);
        pauseButtonAnimator.ResetTrigger(closeTrigger);
    }

    // Unpaused after animation finished
    IEnumerator WaitForAnimation() {
        yield return new WaitForSeconds(1);
        paused = false;
    }

    // Pause/Unpause all audio sources
    void PauseAudio(bool pause) {
        audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioSource in audioSources) {
            if (pause) {
                audioSource.Pause();
            } else {
                audioSource.UnPause();
            }
        }
    }


    // Main menu
    public void GoToMainMenu() {
        Time.timeScale = 1f;
        paused = false;

        StartCoroutine(MainMenuTransition());
    }

    // Transition
    IEnumerator MainMenuTransition() {
        blackTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }


    // About game
    public void AboutGame() {
        aboutPanel.GetComponent<Animator>().SetTrigger(openTrigger);
        aboutPanel.GetComponent<Animator>().ResetTrigger(closeTrigger);
    }

    public void CloseAboutGame() {
        aboutPanel.GetComponent<Animator>().SetTrigger(closeTrigger);
        aboutPanel.GetComponent<Animator>().ResetTrigger(openTrigger);
    }

    // Mute music

    // Mute sounds

}
