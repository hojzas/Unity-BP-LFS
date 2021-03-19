using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task1TakeMobile : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speakingIcon = default;
    [SerializeField] GameObject speechBubbleOneLine = default;

    [TextArea]
    [SerializeField] string taskText = "Vezmi mobil a zavolej pomoc!";

    Animator speechBubbleAnimator;
    Animator speakingIconAnimator;
    TextMeshProUGUI speechBubble;

    void Start()
    {
        speechBubbleAnimator = speechBubbleOneLine.GetComponent<Animator>();
        speakingIconAnimator = speakingIcon.GetComponent<Animator>();
        speechBubble = speechBubbleOneLine.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    internal void StartTask1TakeMobile() {
        StartCoroutine(DisplayTask());
    }

    // Display doggy head and then 1st task speech bubble fill up with text
    private IEnumerator DisplayTask() {

        yield return new WaitForSeconds(1);

        // Head and speech bubble
        speakingIcon.SetActive(true);
        speechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(2);

        GameObject speechBubbleText = speechBubbleOneLine.transform.Find("Text").gameObject;

        // Fill up speech bubble with text
        StartCoroutine(taskManager.WriteText(speechBubble, taskText));
    }

    // Disable 1st task (take mobile) gameobjects
    internal void Disable() {
        speechBubbleAnimator.ResetTrigger("Open");
        speechBubbleAnimator.SetTrigger("Close");
        speakingIconAnimator.SetTrigger("Close");
    }
}
