using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task6TrainSearch : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] PlayerController playerController = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speakingIcon = default;
    [SerializeField] GameObject speechBubbleSmall = default;
    [SerializeField] GameObject speechBubbleBig = default;

    [Header("Passenger counter GUI")]
    [SerializeField] RawImage passengerCounterGUI = default;

    [Header("Gameobject of all the passengers")]
    [SerializeField] GameObject passengers = default;
    [SerializeField] GameObject tapHint = default;

    [Header("Door to next wagon indicator")]
    [SerializeField] GameObject doorIndicator = default;

    [SerializeField] bool isFirstWagonScene = false;
    [SerializeField] bool isLastWagonScene = false;

    [Header("Operator's speech")]
    [TextArea]
    [SerializeField] string question = "Kolik je ve vlaku zraněných?";
    [TextArea]
    [SerializeField] string finalSpeech = "Výborně, pomoc by měla dorazit za pár minut, za chvíli uvidíš přistávat vrtulník, odvedl jsi dobrou práci, děkujeme!";

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    // Static to pass total number to other scenes
    static int totalPassengerFound, totalPassengerNumber = 0;
    internal bool clickingEnable = false;
    TextMeshProUGUI speechBubbleSmallText, speechBubbleBigText, passengerCounterText;
    Animator speechBubbleSmallAnimator, speechBubbleBigAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        speechBubbleSmallText = speechBubbleSmall.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        speechBubbleBigText = speechBubbleBig.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        passengerCounterText = passengerCounterGUI.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        speechBubbleSmallAnimator = speechBubbleSmall.GetComponent<Animator>();
        speechBubbleBigAnimator = speechBubbleBig.GetComponent<Animator>();
    }

    internal void StartTask6TrainSearch() {

        if (isFirstTask()) {
            // First wagon -> start
            StartCoroutine(StartTask());

        } else {
            // Allready finished some wagon(s) -> continue
            ContinueTask();
        }
    }

    IEnumerator StartTask() {
        // Reset counter, bubble text
        totalPassengerNumber = passengers.transform.childCount;
        totalPassengerFound = 0;
        yield return new WaitForSeconds(4);
        taskManager.ResetBubbleText(speechBubbleSmallText);
        yield return new WaitForSeconds(1);

        // Write task question
        StartCoroutine(taskManager.WriteText(speechBubbleSmallText, question));
        yield return new WaitForSeconds(2);

        // Display passenger counter, highlight them, show tap hint
        passengerCounterGUI.gameObject.SetActive(true);
        HighlightPassengers();
        clickingEnable = true;
        playerController.walkEnable = true;

        yield return new WaitForSeconds(2);
        tapHint.SetActive(true);
    }

    void ContinueTask() {
        totalPassengerNumber += passengers.transform.childCount;
        
        // Speech bubble
        speakingIcon.SetActive(true);
        speechBubbleSmallAnimator.SetTrigger(openTrigger);
        speechBubbleSmallText.text = question;

        // Passenger counter
        passengerCounterGUI.gameObject.SetActive(true);
        passengerCounterText.text = totalPassengerFound.ToString();
        HighlightPassengers();
        clickingEnable = true;
    }

    // Highlight all the passengers
    void HighlightPassengers() {
        for (int i = 0; i < passengers.transform.childCount; i++) {
            GameObject passenger = passengers.transform.GetChild(i).gameObject;

            passenger.GetComponent<Animator>().SetTrigger("Highlight");

            // Same for passenger's other part
            Transform otherPart = passenger.transform.Find("OtherPart");
            if (otherPart != null) {
                otherPart.gameObject.GetComponent<Animator>().SetTrigger("Highlight");
            }
        }
    }

    // Passenger counter
    internal void PassengerTap() {
        
        tapHint.SetActive(false);

        // Incrementation
        totalPassengerFound++;
        passengerCounterText.text = totalPassengerFound.ToString();

        // Found all passengers -> enable to go to next wagon if it is't the last one
        if (totalPassengerFound == totalPassengerNumber) {
            if (isLastWagonScene) {
                // Found them all
                StartCoroutine(WriteFinalSpeech());

                // TODO prijezd zachranaru

            } else {
                doorIndicator.SetActive(true);
            }
        }
    }

    IEnumerator WriteFinalSpeech() {
        speechBubbleSmallAnimator.SetTrigger(closeTrigger);
        yield return new WaitForSeconds(1);
        speechBubbleBigAnimator.SetTrigger(openTrigger);
        yield return new WaitForSeconds(1);
        StartCoroutine(taskManager.WriteText(speechBubbleBigText, finalSpeech));
    }

    internal bool isFirstTask() {
        return isFirstWagonScene;
    }
}
