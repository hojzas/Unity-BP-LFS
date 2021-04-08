using UnityEngine;

public class OnWalkingStickClick : MonoBehaviour
{
    [SerializeField] Animator walkingStickanimator = default;

    // Clicking on interactive object, play its animation
    void OnMouseDown() {
        walkingStickanimator.SetTrigger("Play");
    }
}
