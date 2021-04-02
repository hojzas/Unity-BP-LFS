using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWalkingStickClick : MonoBehaviour
{
    [SerializeField] Animator walkingStickanimator;

    // Clicking on interactive object, play its animation
    void OnMouseDown() {
        walkingStickanimator.SetTrigger("Play");
    }
}
