using System.Collections;
using UnityEngine;

public class MoveLandscape : MonoBehaviour
{
    [SerializeField] internal SoundManagement soundManagement = default;

    [Tooltip("Move duration in seconds.")]
    [SerializeField] float moveDuration = 1f;

    [Tooltip("Maximum number of swipes on each side")]
    [SerializeField] int maxSwipes = 3;

    [SerializeField] GameObject swipeHint = default;
    [SerializeField] GameObject tapHint = default;

    [SerializeField] GameObject arrowRight = default;
    [SerializeField] GameObject arrowLeft = default;

    [SerializeField] AudioSource swipeSound = default;

    Vector2 swipeStart, swipeEnd;
    Vector3 landscapePositionStart, landscapePositionEnd;

    float moveTime, transfer;
    internal int currentPosition = 0;
    bool towerClicked = false;
    bool moving = false, swipeLock = false;

    // Update is called once per frame
    void FixedUpdate() {

        // Detect swipe
        if (Input.touchCount > 0 && !moving && !swipeLock) {

            if (Input.GetTouch(0).phase == TouchPhase.Began) {  
                swipeStart = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {  
                swipeEnd = Input.GetTouch(0).position;

                // Get swipe direction
                if (swipeStart.x < swipeEnd.x && currentPosition > -maxSwipes) {
                    // Right
                    StartCoroutine(MoveLandscapeLeft(false));

                } else if (swipeStart.x > swipeEnd.x  && currentPosition < maxSwipes) {
                    // Left
                    StartCoroutine(MoveLandscapeLeft(true));
                }
            }
        }
    }

    // Move landscape left or right
    IEnumerator MoveLandscapeLeft(bool moveLeft) {

        // Landscape is moving
        moving = true;
        moveTime = 0f;
        soundManagement.PlayAudioSource(swipeSound);
        landscapePositionStart = transform.position;
        transfer = Screen.width / 4;

        tapHint.SetActive(false);
        swipeHint.SetActive(false);
        arrowRight.SetActive(false);
        arrowLeft.SetActive(false);

        // Get direction
        if (moveLeft) {
            transfer *= -1;
            currentPosition += 1;
        } else {
            currentPosition -= 1;
        }
        
        // Set target position, modify only x axis
        landscapePositionEnd = transform.position;
        landscapePositionEnd.x = landscapePositionStart.x + transfer;

        // Move at the selected time
        while(moveTime < moveDuration) {
            moveTime += Time.deltaTime;
            transform.position = Vector2.Lerp(landscapePositionStart, landscapePositionEnd, moveTime / moveDuration);
            yield return null;
        }

        // Tower is visible, show hint after few seconds
        if (currentPosition == -2) {
            StartCoroutine(ShowTapHint());
        }

        // Finish
        moving = false;
    }

    IEnumerator ShowTapHint() {
        swipeLock = true;
        yield return new WaitForSeconds(3);

        // Show only when view is static and tower is visible (pos -2)
        if (!IsTowerClicked() && currentPosition == -2 && !moving) {
            tapHint.SetActive(true);
        }
    }

    // Setter towerClicked
    internal void TowerClicked() {
        towerClicked = true;
    }

    // Getter towerClicked
    internal bool IsTowerClicked() {
        return towerClicked;
    }
}
