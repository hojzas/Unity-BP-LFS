using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3LocationSounds : MonoBehaviour
{
    [SerializeField] internal SoundManagement soundManagement = default;

    [SerializeField] AudioSource answersMoveSound = default;

    public void PlayMoveSound() {
        if (!soundManagement.IsSoundMute()) answersMoveSound.Play();
    }
    
}
