using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileArrow : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = default;

    // Clicking on mobile arrow
    void OnMouseDown() {
        playerMovement.SetgoToMobile();
    }
}
