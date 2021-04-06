using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] AudioSource hangingLightAudio = default;

    Animator animator;

    // Lock object
    bool interactiveObjectClickable = true;

    // Animation in progress
    bool animationInProgress = false;

    // Setter
    internal void SetInteractiveObjectClickable(bool state) {
        interactiveObjectClickable = state;
    }

    // Getter
    internal bool IsInteractiveObjectClickable() {
        return interactiveObjectClickable;
    }

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    // Clicking on interactive object, play it's animation
    void OnMouseDown() {
        if (IsInteractiveObjectClickable() && !animationInProgress) {
            animator.SetTrigger("Play");
            animationInProgress = true;
        }
    }

    public void PlayHangingLightAudio() {
        soundManagement.PlayAudioSource(hangingLightAudio);
    }

    public void AnimationFinished() {
        animationInProgress = false;
    }
}
