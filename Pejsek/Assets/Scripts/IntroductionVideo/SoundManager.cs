using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("AudioSource")]
    [SerializeField] AudioSource audioSrc = default;
    
    [Header("Sounds")]
    [SerializeField] AudioClip trainBraking = default;
    [SerializeField] AudioClip trainDepart = default;
    [SerializeField] AudioClip trainDoorOpening = default;
    [SerializeField] AudioClip trainPassingBy = default;


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
        }
    }

    internal void StopAudio() {
        audioSrc.Stop();
    }
}
