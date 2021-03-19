using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickInteractive : MonoBehaviour
{
    Animator animator;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnMouseDown() {
        animator.SetTrigger("Flip");
    }
}
