using UnityEngine;

public class WindowArrow : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = default;

    // Clicking on window or arrow
    void OnMouseDown() {
        playerMovement.SetGoUnderTheWindow();
    }
}
