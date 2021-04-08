using UnityEngine;

public class DoorArrow : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = default;

    // Clicking on door or arrow
    void OnMouseDown() {
        playerMovement.SetgoToTheDoor();
    }
}
