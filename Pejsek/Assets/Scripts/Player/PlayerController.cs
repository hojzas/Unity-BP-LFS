using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *****************************************************************************
// Player controller - connects all player parts
// *****************************************************************************

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] internal PlayerMovement playerMovement = default;
    [SerializeField] internal PlayerCollide playerCollide = default;
    [SerializeField] internal PlayerPickUpMobile playerPickUpMobile = default;
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] internal GameObject player = default;

    internal bool goToNextWagon = false;

    internal bool walkEnable = true;
}
