using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    [SerializeField] GameObject backgroundMusic = default;
    [SerializeField] bool isVideoScene = false;
    [SerializeField] bool isFirstGameScene = false;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSrc = default;
    [SerializeField] AudioClip operator155 = default;
    [SerializeField] AudioClip operator112 = default;
    [SerializeField] AudioClip operator2 = default;
    [SerializeField] AudioClip operator3 = default;
    [SerializeField] AudioClip operator3wrong = default;
    [SerializeField] AudioClip operator4 = default;
    [SerializeField] AudioClip operator5 = default;
    [SerializeField] AudioClip operator6 = default;
    [SerializeField] AudioClip operator7 = default;
    [SerializeField] AudioClip operator8 = default;
    [SerializeField] AudioClip operator9 = default;

    // Static, so the state of sound and music can be load in any scene
    static bool soundIsMute = false, musicIsMute = false, backgroundMusicIsPlaying = false;
    static GameObject backgroundMusicStatic;

    AudioSource[] audioSources;
    static AudioSource backgroundMusicAudio;

    void Awake() {
        if (isFirstGameScene && backgroundMusic != null) {
            backgroundMusicStatic = backgroundMusic;
            backgroundMusicAudio = backgroundMusic.GetComponent<AudioSource>();

            if (!IsVideoScene()) {
                DontDestroyOnLoad(backgroundMusicStatic);
            }
        }
    }

    // Play background music only in game scenes
    void Start() {
        if (!IsMusicMute() && !IsVideoScene() && !IsBackgroundMusicIsPlaying()) {
            PlayBackgroundMusic();
        }
    }

    // IsVideoScene getter
    internal bool IsVideoScene() {
        return isVideoScene;
    }

    internal void PlayBackgroundMusic() {
        if (!isVideoScene) {
            backgroundMusicAudio.Play();
            backgroundMusicIsPlaying = true;
        }
    }

    internal void PauseBackgroundMusic() {
        if (!isVideoScene) {
            backgroundMusicAudio.Pause();
        }
    }

    internal void StopBackgroundMusic() {
        if (!isVideoScene) {
            backgroundMusicAudio.Stop();
            backgroundMusicIsPlaying = false;
            // End of the game or return to main menu -> background music no longer needed
            Destroy(backgroundMusicStatic);
        }
    }

    internal bool IsBackgroundMusicIsPlaying() {
        return backgroundMusicIsPlaying;
    }



    // ************************** Sound **************************
    // soundIsMute getter
    internal bool IsSoundMute() {
        return soundIsMute;
    }

    // soundIsMute setter
    internal void MuteSound(bool state) {
        soundIsMute = state;
    }

    // Play audio if sound is not mute
    internal void PlayAudioSource(AudioSource audioSource) {
        if (!IsSoundMute()) {
            audioSource.Play();
        }
    }

    // Pause/Unpause all audio sources except background music
    internal void PauseAllAudio(bool pause) {
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioSource in audioSources) {
            // Pause all audio - game is paused
            if (pause) {
                audioSource.Pause();

            } else {
                // UnPause all audio - game resume
                if (audioSource == backgroundMusicAudio && IsMusicMute()) {
                    // Keep background music mute
                    continue;
                } else {
                    audioSource.UnPause();
                }
            }    
        }
    }

    // Mute sounds, save and return sound volume values in generic collection
    internal Dictionary<AudioSource, float> MuteSounds()
    {
        var volumes = new Dictionary<AudioSource, float>();

        // Get all audio sources
        audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        // Save and set new volume value, skip only background music
        foreach (AudioSource audioSource in audioSources) {
            volumes.Add(audioSource, audioSource.volume);
            if (audioSource != backgroundMusicAudio) {
                audioSource.volume = 0;
            }
        }

        return volumes;
    }

    // UnMute sounds, set previous volume values
    internal void UnMuteSounds(Dictionary<AudioSource, float> volumes)
    {
        if (volumes != null) {
            foreach(var value in volumes) {
                // Skip background music
                if (value.Key != backgroundMusicAudio) {
                    value.Key.volume = value.Value;
                }
            }
        }
    }

    // ************************** Music **************************
    // MusicIsMute getter
    internal bool IsMusicMute() {
        return musicIsMute;
    }

    // MusicIsMute setter
    internal void MuteMusic(bool state) {
        musicIsMute = state;
    }

    // ************************** Operator **************************
    // Play specific operator audio
    internal void PlayOperatorAudio(string audioClip) {

        if (!IsSoundMute()) {

            switch (audioClip) {
                case "1-112":
                    audioSrc.PlayOneShot(operator112);
                    break;

                case "1-155":
                    audioSrc.PlayOneShot(operator155);
                    break;

                case "2":
                    audioSrc.PlayOneShot(operator2);
                    break;

                case "3":
                    audioSrc.PlayOneShot(operator3);
                    break;

                case "3-wrong":
                    audioSrc.PlayOneShot(operator3wrong);
                    break;

                case "4":
                    audioSrc.PlayOneShot(operator4);
                    break;

                case "5":
                    audioSrc.PlayOneShot(operator5);
                    break;

                case "6":
                    audioSrc.PlayOneShot(operator6);
                    break;

                case "7":
                    audioSrc.PlayOneShot(operator7);
                    break;

                case "8":
                    audioSrc.PlayOneShot(operator8);
                    break;

                case "9":
                    audioSrc.PlayOneShot(operator9);
                    break;
            }
        }
    }
}
