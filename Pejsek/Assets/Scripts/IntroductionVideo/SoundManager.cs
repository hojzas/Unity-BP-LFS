using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("AudioSource")]
    [SerializeField] AudioSource audioSrc = default;
    [SerializeField] internal AudioSource stationNoise = default;
    
    [Header("Sounds")]
    [SerializeField] AudioClip trainBraking = default;
    [SerializeField] AudioClip trainDepart = default;
    [SerializeField] AudioClip trainDoorOpening = default;
    [SerializeField] AudioClip trainPassingBy = default;
    [SerializeField] AudioClip landslide = default;
    [SerializeField] AudioClip cliff = default;
    [SerializeField] AudioClip digging = default;
    [SerializeField] AudioClip trainCrash = default;
    [SerializeField] AudioClip trainPreCrash = default;


    internal void PlayAudio(string audioClip) {
        switch (audioClip) {
            case "trainBraking":
                audioSrc.PlayOneShot(trainBraking);
                break;

            case "trainDepart":
                audioSrc.PlayOneShot(trainDepart);
                break;

            case "trainDoorOpening":
                audioSrc.PlayOneShot(trainDoorOpening);
                break;

            case "trainPassingBy":
                audioSrc.PlayOneShot(trainPassingBy);
                break;

            case "landslide":
                audioSrc.PlayOneShot(landslide);
                break;

            case "digging":
                audioSrc.PlayOneShot(digging);
                break;

            case "cliff":
                audioSrc.PlayOneShot(cliff);
                break;

            case "trainCrash":
                audioSrc.PlayOneShot(trainCrash);
                break;

            case "trainPreCrash":
                audioSrc.PlayOneShot(trainPreCrash);
                break;
        }
    }

    internal void StopAudio() {
        audioSrc.Stop();
    }

    internal void SetVolumeValue(float value) {
        audioSrc.volume = value;
    }

    internal IEnumerator VolumeUp(float increment, float maximum) {
        audioSrc.volume = 0;

        while (audioSrc.volume < maximum) {
            audioSrc.volume += increment;
            yield return null;
        }

    }

    internal IEnumerator VolumeDown(float decrement) {

        while (audioSrc.volume > 0) {
            audioSrc.volume -= decrement;
            yield return null;
        }

        audioSrc.Stop();
    }
}
