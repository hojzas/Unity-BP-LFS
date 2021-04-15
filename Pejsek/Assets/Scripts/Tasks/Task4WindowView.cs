using System.Collections;
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
    [SerializeField] AudioSource landscapeAudio = default;

    [Header("Operator's speech")]
    string question = "Výborně, ale musíme to vědět přesně," + System.Environment.NewLine + "je vidět nějaký výrazný bod?";
    string correct = "Ano, už asi vím, kde jsi. Neboj se," + System.Environment.NewLine + "pomoc je už na cestě.";

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
        playerController.EnableWalk();
        playerController.playerPickUpMobile.mobile.SetActive(false);

        taskManager.ResetBubbleText(speechBubble);

        yield return new WaitForSeconds(1);

        StartCoroutine(taskManager.WriteText(speechBubble, question));
        taskManager.soundManagement.PlayOperatorAudio("4");

        yield return new WaitForSeconds(1);

        windowIndicator.SetActive(true);
        cameraMovement.MoveToLeftWindow();
    }
    
    // Display window view and swipe hint
    internal IEnumerator WindowView() {
        taskManager.interactiveObject.SetInteractiveObjectClickable(false);
        yield return new WaitForSeconds(1);
        windowView.SetActive(true);

        yield return new WaitForSeconds(1);
        taskManager.soundManagement.PlayAudioSource(landscapeAudio);
        swipeHint.SetActive(true);
    }

    // Click on tower 
    public void TowerTap() {

        if (taskManager.moveLandscape.currentPosition == -2) {

            taskManager.soundManagement.PlayAudioSource(tapTowerSound);
            moveLandscape.TowerClicked();
            tapHint.SetActive(false);
            towerButton.enabled = false;
            StartCoroutine(taskManager.WriteText(speechBubble, correct));
            taskManager.soundManagement.PlayOperatorAudio("5");

            StartCoroutine(HideWindow());

            playerController.playerMovement.PlayerFlipX();

            // Next task
            taskManager.task5AppZachranka.StartTask5AppZachranka();
        }
    }

    IEnumerator HideWindow() {
        yield return new WaitForSeconds(6);
        landscapeAudio.Stop();
        windowView.GetComponent<Animator>().Play("Window_end");      
    }
}
