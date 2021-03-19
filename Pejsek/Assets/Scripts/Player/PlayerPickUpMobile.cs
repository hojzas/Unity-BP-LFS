﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpMobile : MonoBehaviour { 

    [SerializeField] PlayerController playerController = default;

    [SerializeField] internal GameObject mobile = default;

    [SerializeField] GameObject mobileScreen = default;

    //[SerializeField] GameObject doggy = default;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Player collides with mobile phone -> stop, pick up and show detailed mobile screen
    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D == mobile.GetComponent<Collider2D>()) {
            playerController.playerMovement.StopAndIdle();
            playerController.playerMovement.target.SetActive(false);

            // Task complete, hide
            playerController.taskManager.task1TakeMobile.Disable();

            playerController.playerMovement.PlayerFlipX();

            // Move mobile to players hand
            mobile.transform.position = new Vector2(transform.position.x - 0.75f, transform.position.y + 1.15f);
            mobile.transform.localScale *= new Vector2(0.9f, 0.9f);
            mobile.GetComponent<Animator>().enabled = false;
            mobile.GetComponent<SpriteRenderer>().flipX = true;
            mobile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
            // Show detailed mobile screen
            mobileScreen.transform.Find("MobileKeyboard").gameObject.SetActive(true);
            mobileScreen.transform.Find("MobileKeyboard").gameObject.GetComponent<Animator>().Play("MobileScreenZoom");
            playerController.walkEnable = false;

            // Display doggy
            /*
            doggy.SetActive(true);
            doggy.GetComponent<Animator>().SetTrigger("Normal");
            doggy.transform.Find("SpeechBubble").gameObject.SetActive(true);
            */
        }
    }
}
