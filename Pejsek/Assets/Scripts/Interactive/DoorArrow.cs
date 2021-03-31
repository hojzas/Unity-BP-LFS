using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorArrow : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = default;

    void OnMouseDown() {
        playerMovement.SetgoToTheDoor();
    }
}
