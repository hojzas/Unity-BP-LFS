using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpMobile : MonoBehaviour { 

    [SerializeField] PlayerController playerController = default;
    [SerializeField] MobileButtonsControl mobileButtonsControl = default;

    [SerializeField] internal GameObject mobile = default;

    [SerializeField] GameObject mobileScreen = default;

    [SerializeField] GameObject doggy = default;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Player collides with mobile phone -> stop, pick up and show detailed mobile screen
    void OnTriggerEnter2D(Collider2D collider2D) {
        if (mobile != null && collider2D == mobile.GetComponent<Collider2D>()) {
            mobileButtonsControl.DisplaySpeechBubble();

            playerController.playerMovement.StopAndIdle();
            playerController.player.SetActive(false);
            playerController.playerMovement.target.SetActive(false);
            playerController.playerMovement.PlayerFlipX();

            // Task complete, hide
            playerController.taskManager.task1TakeMobile.Disable();
            mobile.SetActive(false);
            
            // Show detailed mobile screen
            mobileScreen.transform.Find("MobileKeyboard").gameObject.SetActive(true);
            mobileScreen.transform.Find("MobileKeyboard").gameObject.GetComponent<Animator>().Play("MobileScreenZoom");
            playerController.DisableWalk();

            // Display doggy
            doggy.SetActive(true);
            doggy.GetComponent<Animator>().SetTrigger("Normal");
        }
    }
}
