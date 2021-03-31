using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task4WindowView : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] PlayerController playerController = default;
    [SerializeField] CameraMovement cameraMovement = default;
    [SerializeField] MoveLandscape moveLandscape = default;
    [SerializeField] GameObject doggy = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speechBubbleOneLine = default;

    [Header("Window")]
    [SerializeField] GameObject windowIndicator = default;
    [SerializeField] GameObject windowView = default;
    [SerializeField] GameObject swipeHint = default;
    [SerializeField] GameObject tapHint = default;
    [SerializeField] Button towerButton = default;
    [SerializeField] AudioSource tapTowerSound = default;

    [Header("Operator's speech")]
    string question = "Výborně, ale musíme to vědět přesně," + System.Environment.NewLine + "je vidět nějaký výrazný bod?";
    [TextArea]
    [SerializeField] string correct = "Ano, už asi vím, kde jsi. Pomoc je už na cestě.";

    TextMeshProUGUI speechBubble;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble = speechBubbleOneLine.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    internal void StartTask4WindowView() {
        StartCoroutine(StartTask());
    }

    IEnumerator StartTask() {
        // Enable walk, hide mobile phone
        doggy.SetActive(false);
        playerController.player.SetActive(true);
        playerController.walkEnable = true;
        playerController.playerPickUpMobile.mobile.SetActive(false);

        taskManager.ResetBubbleText(speechBubble);

        yield return new WaitForSeconds(1);

        // TODO taskText null, length check in manager

        StartCoroutine(taskManager.WriteText(speechBubble, question));

        yield return new WaitForSeconds(1);

        windowIndicator.SetActive(true);
        cameraMovement.MoveToLeftWindow();
    }
    
    // Display window view and swipe hint
    internal IEnumerator WindowView() {
        yield return new WaitForSeconds(1);
        windowView.SetActive(true);

        yield return new WaitForSeconds(1);
        swipeHint.SetActive(true);
    }

    // Click on tower 
    public void TowerTap() {

        if (taskManager.moveLandscape.currentPosition == -2) {

            taskManager.soundManagement.PlayAudioSource(tapTowerSound);
            moveLandscape.towerClicked = true;
            tapHint.SetActive(false);
            towerButton.enabled = false;
            StartCoroutine(taskManager.WriteText(speechBubble, correct));
            StartCoroutine(HideWindow());

            playerController.playerMovement.PlayerFlipX();

            // Next task
            taskManager.task5AppZachranka.StartTask5AppZachranka();
        }
    }

    IEnumerator HideWindow() {
        yield return new WaitForSeconds(4);
        windowView.GetComponent<Animator>().Play("Window_end");      
    }
}
