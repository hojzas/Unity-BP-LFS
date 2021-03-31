﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MobileButtonsControl : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] TMP_Text mobileText = default;
    [SerializeField] GameObject doggy = default;
    [SerializeField] Animator MobileTextAnimator = default;

    [Header("Sounds")]
    [SerializeField] AudioSource audioButton = default;
    [SerializeField] AudioSource audioWrongNumber = default;

    bool buttonsEnable = true;

    Animator doggyAnimator;
    Animator mobileAnimator;

    AudioSource audioCall;

    void Start() {
        doggyAnimator = doggy.GetComponent<Animator>();
        mobileAnimator = transform.Find("MobileKeyboard").gameObject.GetComponent<Animator>();
        audioCall = gameObject.GetComponent<AudioSource>();
    }

    // Display clicked button number
    public void DisplayNumber(string number) {
        if (mobileText.text.Length < 9 && buttonsEnable) {
            if (!soundManagement.IsSoundMute()) audioButton.Play();
            mobileText.text += number;
        }
    }

    // Remove last number
    public void Backspace() {
        if(mobileText.text.Length > 0 && buttonsEnable) {
            if (!soundManagement.IsSoundMute()) audioButton.Play();
            mobileText.text = mobileText.text.Remove(mobileText.text.Length - 1, 1);
        }
    }

    // Check corect number and call
    public void Call() {

        if (buttonsEnable) {

            if (!soundManagement.IsSoundMute()) audioButton.Play();

            if(mobileText.text == "155" || mobileText.text == "112") {
                // Correct, call
                EnableButtons(false);
                mobileAnimator.Play("Phone_call");

                if (!soundManagement.IsSoundMute()) audioCall.Play();

                // Doggy feedback
                doggyAnimator.ResetTrigger("Wrong");
                doggyAnimator.ResetTrigger("Normal");
                doggyAnimator.SetTrigger("Correct");

                StartCoroutine(HideMobile());

                bool call155;
                if (mobileText.text == "155") {
                    call155 = true;
                } else {
                    call155 = false;
                }

                // Start next task
                StartCoroutine(taskManager.task2OperatorCallStart.StartCall155(call155));

            } else {
                // Wrong, feedback
                // TODO text
                //doggy.transform.Find(active).gameObject.SetActive(false);
                
                StartCoroutine(WrongNumber());

                // Play wrong answer animation
                doggyAnimator.ResetTrigger("Normal");
                doggyAnimator.SetTrigger("Wrong");
            }
        }
    }

    // Wait few seconds, call connected
    public IEnumerator HideMobile() {
        yield return new WaitForSeconds(6);
        mobileAnimator.Play("MobileScreenZoomOut");
        yield return new WaitForSeconds(1);
        transform.Find("MobileKeyboard").gameObject.SetActive(false);
        audioCall.Stop();
    }

    // Wrong number feedback
    IEnumerator WrongNumber() {
        if (!soundManagement.IsSoundMute()) audioWrongNumber.Play();

        EnableButtons(false);

        MobileTextAnimator.SetTrigger("Flash");
        yield return new WaitForSeconds(1);
        mobileText.text = "";
        EnableButtons(true);
    }

    void EnableButtons(bool state) {
        buttonsEnable = state;
    }
}
