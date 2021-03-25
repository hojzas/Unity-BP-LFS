using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task5AppZachranka : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;
    
    [Header("Speech bubble")]
    [SerializeField] GameObject speechBubbleOneLine = default;
    [SerializeField] GameObject speechBubble = default;

    [Header("Mobiles")]
    [SerializeField] GameObject mobile = default;
    [SerializeField] GameObject mobileHome = default;
    [SerializeField] GameObject mobileApp = default;
    [SerializeField] GameObject mobileAppCalling = default;
    [SerializeField] GameObject holdHint = default;
    [SerializeField] Image loadingCircle = default;
    [SerializeField] float callDuration = 3f;

    [Header("Operator's speech")]
    [TextArea]
    [SerializeField] string question = "Ale abychom si byli jistí, zkus zavolat z aplikace Záchranka.";
    [TextArea]
    [SerializeField] string feedback = "Tak a teď už vidím, kde přesně k nehodě došlo.";

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    float loading = 0f;
    float period;
    bool holdingCallButton, callComplete = false;
    Animator speechBubbleOneLineAnimator, speechBubbleAnimator;
    TextMeshProUGUI speechBubbleOneLineText, speechBubbleText;

    // Start is called before the first frame update
    void Start() {
        speechBubbleOneLineAnimator = speechBubbleOneLine.GetComponent<Animator>();
        speechBubbleAnimator = speechBubble.GetComponent<Animator>();
        speechBubbleOneLineText = speechBubbleOneLine.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        speechBubbleText = speechBubble.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        // Fill amount maximum is 1
        // Loading call should take 3s
        // Fixed update is called every 0.02s -> 1/(3/0.02)
        period = 1 / (callDuration / 0.02f);
    }

    void FixedUpdate() {
        if (holdingCallButton) {
            // Hide hint
            holdHint.SetActive(false);

            loading += period;

            if (loading >= 1) {
                holdingCallButton = false;
                StartCoroutine(CallComplete());
            }

            loadingCircle.fillAmount = loading;
        }
    }

    internal void StartTask5AppZachranka() {
        StartCoroutine(StartTask());
    }

    IEnumerator StartTask() {

        // Open different speech bubble
        yield return new WaitForSeconds(3);
        speechBubbleOneLineAnimator.ResetTrigger(openTrigger);
        speechBubbleOneLineAnimator.SetTrigger(closeTrigger);
        yield return new WaitForSeconds(2);
        speechBubbleAnimator.SetTrigger(openTrigger);
        yield return new WaitForSeconds(1);

        // Write task
        StartCoroutine(taskManager.WriteText(speechBubbleText, question));
        yield return new WaitForSeconds(4);

        // Show up mobile screen
        mobile.SetActive(true);
        mobileHome.SetActive(true);

        StartCoroutine(ShowHint());
    }

    // 1st part
    // App icon on click
    public void RunApp() {
        holdHint.SetActive(false);
        
        mobileHome.SetActive(false);
        mobileApp.SetActive(true);

        StartCoroutine(ShowHint());
    }

    // 2nd part
    // Call complete
    IEnumerator CallComplete() {
        callComplete = true;
        mobileApp.SetActive(false);
        mobileAppCalling.SetActive(true);

        yield return new WaitForSeconds(3);

        // Task complete
        mobileAppCalling.GetComponent<Animator>().Play("MobileScreenAppZoomOut");

        // Close speech bubble
        taskManager.ResetBubbleText(speechBubbleText);
        speechBubbleAnimator.SetTrigger(closeTrigger);
        yield return new WaitForSeconds(1);

        mobile.SetActive(false);

        // Feedback
        taskManager.ResetBubbleText(speechBubbleOneLineText);
        speechBubbleOneLineAnimator.SetTrigger(openTrigger);
        speechBubbleOneLineAnimator.ResetTrigger(closeTrigger);
        
        yield return new WaitForSeconds(1);
        StartCoroutine(taskManager.WriteText(speechBubbleOneLineText, feedback));

        // Next task
        taskManager.task6TrainSearch.StartTask6TrainSearch();
    }

    // Call button pressed and hold
    public void CallButtonDown() {
        holdingCallButton = true;
    }

    // Call button up
    public void CallButtonUp() {
        holdingCallButton = false;
        holdHint.SetActive(true);
        loading = 0f;
        loadingCircle.fillAmount = loading;
    }

    // Show hint 
    IEnumerator ShowHint() {
        yield return new WaitForSeconds(5);
        if (!holdingCallButton && !callComplete) {
            holdHint.SetActive(true);
        }
    }
}
