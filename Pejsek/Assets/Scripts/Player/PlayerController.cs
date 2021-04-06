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

    bool goToNextWagon = false;

    bool walkEnable = true;

    // Setter go to next wagon
    internal void SetGoToNextWagon() {
        goToNextWagon = true;
    }

    // Getter goToNextWagon
    internal bool GoToNextWagon() {
        return goToNextWagon;
    }


    // Setter walk enable
    internal void EnableWalk() {
        walkEnable = true;
    }

    // Setter walk disable
    internal void DisableWalk() {
        walkEnable = false;
    }

    // Getter walkEnable
    internal bool IsWalkEnable() {
        return walkEnable;
    }


}
