using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("AudioSource")]
    [SerializeField] AudioSource audioSrc = default;
    [SerializeField] internal AudioSource stationNoise = default;
    [SerializeField] SoundManagement soundManagement = default;
    
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
    [SerializeField] AudioClip helicopterTakeOff = default;
    [SerializeField] AudioClip birdSing = default;
    [SerializeField] AudioClip ambulance = default;
    [SerializeField] AudioClip doggyOutro = default;


    // Play specific audio
    internal void PlayAudio(string audioClip) {

        if (!soundManagement.IsSoundMute()) {

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

                case "helicopterTakeOff":
                    audioSrc.PlayOneShot(helicopterTakeOff);
                    break;

                case "birdSing":
                    audioSrc.PlayOneShot(birdSing);
                    break;

                case "ambulance":
                    audioSrc.PlayOneShot(ambulance);
                    break;

                case "doggyOutro":
                    audioSrc.PlayOneShot(doggyOutro);
                    break;
            }
        }
    }

    internal void PlayStationNoise() {

        if (!soundManagement.IsSoundMute()) {
            stationNoise.Play();
        }
    }

    // Stop all audios
    internal void StopAudio() {
        audioSrc.Stop();
    }

    // Set volume to value <0, 1>
    internal void SetVolumeValue(float value) {
        if (!soundManagement.IsSoundMute()) {
            audioSrc.volume = value;
        }
    }

    // Set volume up slowly (by increment) below maximum value
    internal IEnumerator VolumeUp(float increment, float maximum) {
        audioSrc.volume = 0;

        if (!soundManagement.IsSoundMute()) {
            while (audioSrc.volume < maximum) {
                audioSrc.volume += increment;
                yield return null;
            }
        }

    }

    // Set volume down by decrement
    internal IEnumerator VolumeDown(float decrement) {

        if (!soundManagement.IsSoundMute()) {
            while (audioSrc.volume > 0) {
                audioSrc.volume -= decrement;

                if (stationNoise != null) stationNoise.volume -= decrement;

                yield return null;
            }
        }

        audioSrc.Stop();
    }
}
