using UnityEngine;

// *****************************************************************************
// Player movement - player goes to the touch position
// *****************************************************************************

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController playerController = default;
    
    [SerializeField] float moveSpeed = 6.5f;

    [SerializeField] Collider2D walkArea = default;
    [SerializeField] Transform underWindow = default;
    [SerializeField] Transform nearDoor = default;

    [Header("Animations")]
    [SerializeField] AnimationClip animationIdle = default;
    [SerializeField] AnimationClip animationRun = default;

    [SerializeField] internal GameObject target = default;

    bool isMoving = false;
    bool goUnderTheWindow, goToTheDoor = false;
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

    // Getter isMoving
    internal bool IsMoving() {
        return isMoving;
    }

    // Setter isMoving
    internal void StartMoving() {
        isMoving = true;
    }

    // Setter isMoving
    internal void StopMoving() {
        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (IsMoving()) {
            // Get current player position
            currentPosition = (touchPosition - transform.position).magnitude;
        }

        if (Input.touchCount > 0 && playerController.IsWalkEnable()) {

            // New touch detected
            touch = Input.GetTouch(0);

            // Check if the touch is in allowed walk area
            if (walkArea.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position))) {

                // Move only where the first touch has been detected
                if (touch.phase == TouchPhase.Began) {

                    // Get touch position                        
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Touch effect
                    target.SetActive(true);
                    touchPosition.z = 10;
                    target.transform.position = touchPosition;

                    previousPosition = currentPosition = 0;
                    touchPosition.z = 0;
                    animator.Play(animationRun.name);
                    StartMoving();

                    destination = (touchPosition - transform.position).normalized;

                    // Check player sprite flip
                    CheckAxisXFlip();

                    // Move player
                    rigidb2D.velocity = new Vector2(destination.x * moveSpeed, destination.y * moveSpeed);
                }
            }
        }

        // Fixed destination set
        // When clicking on the arrow, window or door (these objects are not in clickable allowed walk area)
        if (goUnderTheWindow || goToTheDoor) {

            if (goUnderTheWindow) {
                touchPosition = underWindow.position;
                destination = (underWindow.position - transform.position).normalized;
            } else {
                touchPosition = nearDoor.position;
                destination = (nearDoor.position - transform.position).normalized;
            }

            // Same as above
            previousPosition = currentPosition = 0;
            StartMoving();
            animator.Play(animationRun.name);

            CheckAxisXFlip();

            rigidb2D.velocity = new Vector2(destination.x * moveSpeed, destination.y * moveSpeed);

            // Reset
            goUnderTheWindow = goToTheDoor = false;
        }

        // Destination is reached
        if (currentPosition > previousPosition) {

            if (playerController.GoToNextWagon()) {
                Stop();
                animator.Play(playerController.playerCollide.animationClipFade.name);
            } else {
                StopAndIdle();
            }
        }

        if (IsMoving()) {
            previousPosition = (touchPosition - transform.position).magnitude;
        }
    }

    // Check player sprite flip
    void CheckAxisXFlip() {
        if (destination.x < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    // Player stop and play idle animation
    internal void StopAndIdle() {
        HideTarget();
        StopMoving();
        rigidb2D.velocity = Vector2.zero;
        animator.Play(animationIdle.name);
    }

    // Player stop
    internal void Stop() {
        HideTarget();
        StopMoving();
        rigidb2D.velocity = Vector2.zero;
    }

    internal void HideTarget() {
        target.SetActive(false);
    }

    internal void PlayerFlipX() {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // Setter goUnderTheWindow
    internal void SetGoUnderTheWindow() {
        goUnderTheWindow = true;
    }

    // Setter goToTheDoor
    internal void SetgoToTheDoor() {
        goToTheDoor = true;
    }
}
