using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickInteractive : MonoBehaviour
{
    Animator animator;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    // Clicking on interactive object, play its animation
    void OnMouseDown() {
        animator.SetTrigger("Play");
    }
}
