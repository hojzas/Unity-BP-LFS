using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowArrow : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = default;

    void OnMouseDown() {
        playerMovement.SetGoUnderTheWindow();
    }
}
