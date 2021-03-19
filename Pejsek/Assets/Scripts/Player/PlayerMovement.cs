using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *****************************************************************************
// Player movement - player goes to the touch position
// *****************************************************************************

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController playerController = default;
    
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Collider2D walkArea = default;

    [Header("Animations")]
    [SerializeField] AnimationClip animationIdle = default;
    [SerializeField] AnimationClip animationRun = default;

    [SerializeField] internal GameObject target = default;

    internal bool isMoving; //destinationReached = false;
    Touch touch;
    Vector3 touchPosition, destination;

    float previousPosition, currentPosition;

    Animator animator;
    Rigidbody2D rigidb2D;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rigidb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(isMoving) {
            // Get current player position
            currentPosition = (touchPosition - transform.position).magnitude;
        }

        if(Input.touchCount > 0 && playerController.walkEnable) {

            // New touch detected
            touch = Input.GetTouch(0);

            // Check if the touch is in allowed walk area
            if (walkArea.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position))) {

                // Move only where the first touch has been detected
                if(touch.phase == TouchPhase.Began) {

                    // Get touch position                        
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Touch effect
                    target.SetActive(true);
                    touchPosition.z = 10;
                    target.transform.position = touchPosition;

                    previousPosition = currentPosition = 0;
                    touchPosition.z = 0;
                    animator.Play(animationRun.name);
                    isMoving = true;

                    destination = (touchPosition - transform.position).normalized;

                    // Check player sprite flip
                    if (destination.x < 0) {
                        spriteRenderer.flipX = true;
                    } else {
                        spriteRenderer.flipX = false;
                    }

                    // Move player, speed changes depending on the distance (scale)
                    rigidb2D.velocity = new Vector2(destination.x * moveSpeed, destination.y * moveSpeed);
                }
            }            
        }

        // Destination is reached
        if(currentPosition > previousPosition) {

            if (playerController.goToNextWagon) {
                Stop();
                animator.Play(playerController.playerCollide.animationClipFade.name);
            } else {
                StopAndIdle();
            }
        }

        if(isMoving) {
            previousPosition = (touchPosition - transform.position).magnitude;
        }
    }

    internal void SpeedDown() {
        rigidb2D.velocity = new Vector2(rigidb2D.velocity.x * 0.5f, rigidb2D.velocity.y * 0.5f);
    }

    internal void SpeedUp() {
        rigidb2D.velocity = new Vector2(rigidb2D.velocity.x * 1.5f, rigidb2D.velocity.y * 1.5f);
    }

    internal void StopAndIdle() {
        HideTarget();
        isMoving = false;
        rigidb2D.velocity = Vector2.zero;
        animator.Play(animationIdle.name);
    }

    internal void Stop() {
        HideTarget();
        isMoving = false;
        rigidb2D.velocity = Vector2.zero;
    }

    internal void HideTarget() {
        target.SetActive(false);
    }

    internal void PlayerFlipX() {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
