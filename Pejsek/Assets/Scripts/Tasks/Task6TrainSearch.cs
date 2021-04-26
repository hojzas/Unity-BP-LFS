using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task6TrainSearch : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] PlayerController playerController = default;
    [SerializeField] SceneController sceneController = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speakingIcon = default;
    [SerializeField] GameObject speechBubbleSmall = default;
    [SerializeField] GameObject speechBubbleBig = default;

    [Header("Passenger counter")]
    [SerializeField] RawImage passengerCounterGUI = default;
    [SerializeField] Animator passengerCounterAnimator = default;

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
    bool clickingEnable = false;
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

        if (IsFirstTask()) {
            // First wagon -> start
            StartCoroutine(StartTask());

        } else {
            // Allready finished some wagon(s) -> continue
            ContinueTask();
        }
    }

    IEnumerator StartTask() {
        // Reset counter, bubble text, enable interactive objects
        totalPassengerNumber = passengers.transform.childCount;
        totalPassengerFound = 0;
        taskManager.interactiveObject.SetInteractiveObjectClickable(true);
        yield return new WaitForSeconds(4);
        taskManager.ResetBubbleText(speechBubbleSmallText);
        yield return new WaitForSeconds(1);

        // Write task question
        StartCoroutine(taskManager.WriteText(speechBubbleSmallText, question));
        taskManager.soundManagement.PlayOperatorAudio("8");
        yield return new WaitForSeconds(2);

        // Display passenger counter, highlight them, show tap hint
        passengerCounterGUI.gameObject.SetActive(true);
        HighlightPassengers();
        EnableClicking();
        playerController.EnableWalk();

        yield return new WaitForSeconds(2);
        if (totalPassengerFound <= totalPassengerNumber) {
            tapHint.SetActive(true);
        }
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
        EnableClicking();
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
    internal IEnumerator PassengerTap() {
        
        // Hide hint, play counter plus animation
        tapHint.SetActive(false);
        passengerCounterAnimator.SetTrigger("PlusOne");
        yield return new WaitForSeconds(0.3f);

        // Incrementation
        totalPassengerFound++;
        passengerCounterText.text = totalPassengerFound.ToString();

        // Found all passengers -> enable to go to next wagon if it is't the last one
        if (totalPassengerFound == totalPassengerNumber) {
            if (isLastWagonScene) {
                // Found them all
                StartCoroutine(WriteFinalSpeechAndLoadNextScene());
            } else {
                doorIndicator.SetActive(true);
            }
        }
    }

    IEnumerator WriteFinalSpeechAndLoadNextScene() {
        speechBubbleSmallAnimator.SetTrigger(closeTrigger);
        yield return new WaitForSeconds(1);
        speechBubbleBigAnimator.SetTrigger(openTrigger);
        yield return new WaitForSeconds(1);
        StartCoroutine(taskManager.WriteText(speechBubbleBigText, finalSpeech));
        taskManager.soundManagement.PlayOperatorAudio("9");

        yield return new WaitForSeconds(10);

        taskManager.soundManagement.StopBackgroundMusic();

        StartCoroutine(sceneController.LoadNextScene(true));
    }

    internal bool IsFirstTask() {
        return isFirstWagonScene;
    }

    // Setter clicking enable
    internal void EnableClicking() {
        clickingEnable = true;
    }

    // Setter clicking disable
    internal void DisableeClicking() {
        clickingEnable = false;
    }

    internal bool IsClickingEnable() {
        return clickingEnable;
    }
}
