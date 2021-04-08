using UnityEngine;

public class Task3LocationSounds : MonoBehaviour
{
    [SerializeField] internal SoundManagement soundManagement = default;

    [SerializeField] AudioSource answersMoveSound = default;

    public void PlayMoveSound() {
        soundManagement.PlayAudioSource(answersMoveSound);
    }
    
}
