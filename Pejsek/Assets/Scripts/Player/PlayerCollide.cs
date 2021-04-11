using System.Collections;
using UnityEngine;

public class PlayerCollide : MonoBehaviour { 

    [SerializeField] PlayerController playerController = default;
    [SerializeField] SceneController sceneController = default;

    [SerializeField] GameObject doorIndicator = default;
    [SerializeField] GameObject windowIndicator = default;

    [SerializeField] internal AnimationClip animationClipFade = default;

    // Player enters collider
    void OnTriggerEnter2D(Collider2D collider2D) {

        // Collides with door -> goes to next scene (wagon)
        if (doorIndicator != null && collider2D == doorIndicator.GetComponent<Collider2D>()) {
            playerController.SetGoToNextWagon();
            playerController.DisableWalk();
            doorIndicator.SetActive(false);

        // Enter window view
        } else if (windowIndicator != null && collider2D == windowIndicator.GetComponent<Collider2D>()) {
            playerController.DisableWalk();
            windowIndicator.SetActive(false);

            // Run window view task via taskManager
            StartCoroutine(playerController.taskManager.task4WindowView.WindowView());

        // Collides with passenger -> stop, cannot go through
        } else {
            ProcessPassengerCollision(collider2D.gameObject);               
        }
    }

    // Player stays
    void OnTriggerStay2D(Collider2D collider2D) {
        ProcessPassengerCollision(collider2D.gameObject);
    }

    void ProcessPassengerCollision(GameObject collider) {
        if (collider.CompareTag("Passenger")) {
            playerController.playerMovement.StopAndIdle();
        }
    }

    // Next wagon scene, called after end of player fade animation
    void goToNextWagon() {
        StartCoroutine(sceneController.LoadNextScene(true));
    }
}
