using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// *****************************************************************************
// Pause menu
// *****************************************************************************

public class PauseMenu : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] SceneController sceneController = default;


    [Header("Pause menu settings")]
    [SerializeField] Button pauseButton = default;
    [SerializeField] GameObject aboutPanel = default;
    [SerializeField] Animator pauseMenuAnimator = default;
    [SerializeField] Animator skipButtonAnimator = default;

    [Header("Buttons")]
    [SerializeField] GameObject muteMusicButton = default;
    [SerializeField] GameObject unMuteMusicButton = default;
    [SerializeField] GameObject muteSoundsButton = default;
    [SerializeField] GameObject unMuteSoundsButton = default;

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    Animator pauseButtonAnimator;
    bool paused = false;
    bool menuDisabled = false;

    Dictionary<AudioSource,float> sourcesAndVolumes;

    void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);

        pauseButtonAnimator = pauseButton.gameObject.GetComponent<Animator>();
    }

    // Pause the game when pressing Android's back button
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    void DisableMenu(bool state) {
        menuDisabled = state;
    }

    // Pause
    public void PauseGame() {
        DisableMenu(false);

        Time.timeScale = 0f;

        soundManagement.PauseAllAudio(true);

        GetSoundAndMusicButtons();

        // Open pause menu
        pauseMenuAnimator.SetTrigger(openTrigger);
        pauseMenuAnimator.ResetTrigger(closeTrigger);
        SetPause();

        // Close pause button
        pauseButtonAnimator.SetTrigger(closeTrigger);
        pauseButtonAnimator.ResetTrigger(openTrigger);

        if (skipButtonAnimator != null) {
            skipButtonAnimator.SetTrigger(closeTrigger);
            skipButtonAnimator.ResetTrigger(openTrigger);
        }
    }

    // Display the current state of the sound and music buttons
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

            DisableMenu(true);

            Time.timeScale = 1f;

            soundManagement.PauseAllAudio(false);

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
        SetResume();
    }

    


    // Main menu
    public void GoToMainMenu() {

        if (!menuDisabled) {

            DisableMenu(true);

            Time.timeScale = 1f;
            SetPause();

            soundManagement.StopBackgroundMusic();

            // Main menu transition
            StartCoroutine(sceneController.LoadMainMenuScene(true));
        }
    }


    // About game
    public void AboutGame() {
        
        if (!menuDisabled) {

            DisableMenu(true);
            aboutPanel.GetComponent<Animator>().SetTrigger(openTrigger);
            aboutPanel.GetComponent<Animator>().ResetTrigger(closeTrigger);
        }
    }

    // Close about game
    public void CloseAboutGame() {
        DisableMenu(false);
        aboutPanel.GetComponent<Animator>().SetTrigger(closeTrigger);
        aboutPanel.GetComponent<Animator>().ResetTrigger(openTrigger);
    }

    // Mute/unMute music, switch buttons in pause menu
    public void MuteMusic(bool mute) {
        if (!menuDisabled) {
            if (mute) {
                soundManagement.MuteMusic(true);
                muteMusicButton.SetActive(false);
                unMuteMusicButton.SetActive(true);

                soundManagement.PauseBackgroundMusic();
            } else {
                soundManagement.MuteMusic(false);
                unMuteMusicButton.SetActive(false);
                muteMusicButton.SetActive(true);

                soundManagement.PlayBackgroundMusic();
                // Immediately pause because game is paused
                soundManagement.PauseBackgroundMusic();
            }
        }
    }

    // Mute/unMute sounds, switch buttons in pause menu
    public void MuteSound(bool mute) {
        if (!menuDisabled) {
            if (mute) { 
                soundManagement.MuteSound(true);
                muteSoundsButton.SetActive(false);
                unMuteSoundsButton.SetActive(true);
                // Mute all sounds, save their audio source volume
                sourcesAndVolumes = soundManagement.MuteSounds();

            } else {
                soundManagement.MuteSound(false);
                unMuteSoundsButton.SetActive(false);
                muteSoundsButton.SetActive(true);
                soundManagement.UnMuteSounds(sourcesAndVolumes);
            }
        }
    }

    // Setter pause
    internal void SetPause() {
        paused = true;
    }

    // Setter resume
    internal void SetResume() {
        paused = false;
    }

    internal bool IsGamePaused() {
        return paused;
    }
}
