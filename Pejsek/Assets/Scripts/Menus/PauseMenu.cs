using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// *****************************************************************************
// Pause menu
// *****************************************************************************

public class PauseMenu : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;

    [Header("Pause menu settings")]
    [SerializeField] Button pauseButton = default;
    [SerializeField] GameObject aboutPanel = default;
    [SerializeField] Animator pauseMenuAnimator = default;
    [SerializeField] Animator skipButtonAnimator = default;

    [SerializeField] Animator blackTransition = default;

    [Header("Buttons")]
    [SerializeField] GameObject muteMusicButton = default;
    [SerializeField] GameObject unMuteMusicButton = default;
    [SerializeField] GameObject muteSoundsButton = default;
    [SerializeField] GameObject unMuteSoundsButton = default;

     [Header("Background music audio source")]
    [SerializeField] AudioSource backgroundMusic = default;

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    Animator pauseButtonAnimator;
    internal bool paused = false;

    bool menuDisabled = false;

    Dictionary<AudioSource,float> sourcesAndVolumes;

    void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);

        pauseButtonAnimator = pauseButton.gameObject.GetComponent<Animator>();
    }

    // Pause
    void PauseGame() {
        menuDisabled = false;

        Time.timeScale = 0f;

        PauseAudio(true);

        GetSoundAndMusicButtons();

        // Open pause menu
        pauseMenuAnimator.SetTrigger(openTrigger);
        pauseMenuAnimator.ResetTrigger(closeTrigger);
        paused = true;

        // Close pause button
        pauseButtonAnimator.SetTrigger(closeTrigger);
        pauseButtonAnimator.ResetTrigger(openTrigger);

        if (skipButtonAnimator != null) {
            skipButtonAnimator.SetTrigger(closeTrigger);
            skipButtonAnimator.ResetTrigger(openTrigger);
        }
    }

    void GetSoundAndMusicButtons() {
        if (soundManagement.IsSoundMute()) {
            muteSoundsButton.SetActive(false);
            unMuteSoundsButton.SetActive(true);
        } else {
            unMuteSoundsButton.SetActive(false);
            muteSoundsButton.SetActive(true);
        }

        if (soundManagement.IsMusicMute()) {
            muteMusicButton.SetActive(false);
            unMuteMusicButton.SetActive(true);
        } else {
            unMuteMusicButton.SetActive(false);
            muteMusicButton.SetActive(true);
        }
    }

    // Resume
    public void ResumeGame() {

        if (!menuDisabled) {

            menuDisabled = true;

            Time.timeScale = 1f;

            PauseAudio(false);

            pauseMenuAnimator.SetTrigger(closeTrigger);
            pauseMenuAnimator.ResetTrigger(openTrigger);
            StartCoroutine(WaitForAnimation());

            if (skipButtonAnimator == null) {
                pauseButtonAnimator.SetTrigger(openTrigger);
                pauseButtonAnimator.ResetTrigger(closeTrigger);
            }
        }
    }

    // Unpaused after animation finished
    IEnumerator WaitForAnimation() {
        yield return new WaitForSeconds(1);
        paused = false;
    }

    // Pause/Unpause all audio sources except background music
    void PauseAudio(bool pause) {
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
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

        if (!menuDisabled) {

            menuDisabled = true;

            Time.timeScale = 1f;
            paused = false;

            StartCoroutine(MainMenuTransition());
        }
    }

    // Transition
    IEnumerator MainMenuTransition() {
        blackTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }


    // About game
    public void AboutGame() {
        
        if (!menuDisabled) {

            menuDisabled = true;
            aboutPanel.GetComponent<Animator>().SetTrigger(openTrigger);
            aboutPanel.GetComponent<Animator>().ResetTrigger(closeTrigger);
        }
    }

    public void CloseAboutGame() {
        menuDisabled = false;
        aboutPanel.GetComponent<Animator>().SetTrigger(closeTrigger);
        aboutPanel.GetComponent<Animator>().ResetTrigger(openTrigger);
    }

    // Mute/unMute music, switch buttons in pause menu
    public void MuteMusic(bool mute) {
        if (mute) {
            soundManagement.MuteMusic(true);
            muteMusicButton.SetActive(false);
            unMuteMusicButton.SetActive(true);

            backgroundMusic.Stop();
        } else {
            soundManagement.MuteMusic(false);
            unMuteMusicButton.SetActive(false);
            muteMusicButton.SetActive(true);

            backgroundMusic.Play();
            // Immediately pause because game is paused
            backgroundMusic.Pause();
        }
    }

    // Mute/unMute sounds, switch buttons in pause menu
    public void MuteSound(bool mute) {
        if (mute) {
            soundManagement.MuteSound(true);
            muteSoundsButton.SetActive(false);
            unMuteSoundsButton.SetActive(true);
            // Mute all sounds, save their audio source volume
            sourcesAndVolumes = soundManagement.MuteSounds(backgroundMusic);

        } else {
            soundManagement.MuteSound(false);
            unMuteSoundsButton.SetActive(false);
            muteSoundsButton.SetActive(true);
            soundManagement.UnMuteSounds(sourcesAndVolumes, backgroundMusic);
        }
    }


                                                                    public void SkipToDelete() {
                                                                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                                                                    }

}
