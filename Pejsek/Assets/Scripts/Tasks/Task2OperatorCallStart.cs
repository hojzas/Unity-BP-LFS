using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task2OperatorCallStart : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speakingIcon = default;
    [SerializeField] GameObject speechBubbleOneLine = default;

    [Header("Operator's speech")]
    [SerializeField] string introducePart1 = "Dobrý den, ";
    [SerializeField] string introducePart2 = ", jak vám mohu pomoci?";
    [SerializeField] string introduce155 = "zdravotnická záchranná služba";
    [SerializeField] string introduce112 = "tísňová linka";
    [TextArea]
    [SerializeField] string afterCorrectAnswer = "Dobře, neboj, společně všem zraněným pasažérům pomůžeme.";

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    Animator speechBubbleAnimator;
    Animator speakingIconAnimator;
    TextMeshProUGUI speechBubble;

    void Awake()
    {
        speechBubbleAnimator = speechBubbleOneLine.GetComponent<Animator>();
        speakingIconAnimator = speakingIcon.GetComponent<Animator>();
        speechBubble = speechBubbleOneLine.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    internal IEnumerator StartCall155(bool call155) {

        taskManager.ResetBubbleText(speechBubble);

        yield return new WaitForSeconds(6);

        speakingIcon.SetActive(true);
        speechBubbleAnimator.SetTrigger(openTrigger);

        yield return new WaitForSeconds(2);

        // Create introduce
        string introduce = introducePart1;

        if (call155) {
            introduce += introduce155 + introducePart2;
        } else {
            introduce += introduce112 + introducePart2;
        }

        StartCoroutine(taskManager.WriteText(speechBubble, introduce));

        // TODO: popsat situaci
        yield return new WaitForSeconds(5);
        StartCoroutine(taskManager.WriteText(speechBubble, "TODO: Popsat situaci"));
        yield return new WaitForSeconds(3);



        // Correct
        StartCoroutine(taskManager.WriteText(speechBubble, afterCorrectAnswer));

        yield return new WaitForSeconds(5);

        // Next task      
        taskManager.task3Location.StartTask3Location();
    }    

    // Disable 2st task (call with operator) gameobjects
    internal void Disable() {
        speechBubbleAnimator.ResetTrigger(openTrigger);
        speechBubbleAnimator.SetTrigger(closeTrigger);
        speakingIconAnimator.SetTrigger(closeTrigger);
    }
}
