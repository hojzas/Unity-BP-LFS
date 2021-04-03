using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickInteractive : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] AudioSource hangingLightAudio = default;

    Animator animator;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    // Clicking on interactive object, play its animation
    void OnMouseDown() {
        animator.SetTrigger("Play");
    }

    public void PlayHangingLightAudio() {
        soundManagement.PlayAudioSource(hangingLightAudio);
    }
}
