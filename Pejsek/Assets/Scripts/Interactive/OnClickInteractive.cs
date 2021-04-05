using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickInteractive : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] AudioSource hangingLightAudio = default;

    Animator animator;

    bool interactiveObjectClickable = true;

    internal void SetInteractiveObjectClickable(bool state) {
        interactiveObjectClickable = state;
    }

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    // Clicking on interactive object, play its animation
    void OnMouseDown() {
        if (interactiveObjectClickable) {
            animator.SetTrigger("Play");
        }
    }

    public void PlayHangingLightAudio() {
        soundManagement.PlayAudioSource(hangingLightAudio);
    }
}
