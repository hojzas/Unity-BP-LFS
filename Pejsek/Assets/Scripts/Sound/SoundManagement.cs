using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic = default;
    [SerializeField] bool isVideoScene = false;

    // Static, so the state of sound and music can be load in every scene
    static bool soundIsMute = false;
    static bool musicIsMute = false;

    AudioSource[] audioSources;

    void Start() {
        if (!IsMusicMute() && !IsVideoScene()) {
            backgroundMusic.Play();
        }
    }

    // IsVideoScene getter
    internal bool IsVideoScene() {
        return isVideoScene;
    }


    // ************* Sound *************
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

    // Mute sounds, save and return sound volume values
    internal Dictionary<AudioSource, float> MuteSounds(AudioSource backgroundMusic)
    {
        var volumes = new Dictionary<AudioSource, float>();
        audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        // Save and set new volume value, skip only background music
        foreach (AudioSource audioSource in audioSources) {
            volumes.Add(audioSource, audioSource.volume);
            if (audioSource != backgroundMusic) {
                audioSource.volume = 0;
            }
        }

        return volumes;
    }

    // UnMute sounds, set previous volume values
    internal void UnMuteSounds(Dictionary<AudioSource, float> volumes, AudioSource backgroundMusic)
    {
        if (volumes != null) {
            foreach(var value in volumes) {
                // Skip background music
                if (value.Key != backgroundMusic) {
                    value.Key.volume = value.Value;
                }
            }
        }
    }

    // ************* Music *************
    // musicIsMute getter
    internal bool IsMusicMute() {
        return musicIsMute;
    }

    // musicIsMute setter
    internal void MuteMusic(bool state) {
        musicIsMute = state;
    }
}
