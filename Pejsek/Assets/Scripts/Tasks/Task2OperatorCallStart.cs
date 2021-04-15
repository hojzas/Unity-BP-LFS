using System.Collections;
using UnityEngine;
using TMPro;

public class Task2OperatorCallStart : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speakingIcon = default;
    [SerializeField] GameObject speechBubbleOneLine = default;
    [SerializeField] GameObject speechBubbleDoggy = default;

    [Header("Operator's speech")]
    [SerializeField] string introducePart1 = "Dobrý den, ";
    [SerializeField] string introducePart2 = ", jak vám mohu pomoci?";
    [SerializeField] string introduce155 = "zdravotnická záchranná služba";
    [SerializeField] string introduce112 = "tísňová linka";
    [SerializeField] AudioSource doggyCorrect = default;
    [TextArea]
    [SerializeField] string afterCorrectAnswer = "Dobře, neboj, společně všem zraněným pasažérům pomůžeme.";

    [Header("Doggy's response")]
    [TextArea]
    [SerializeField] string doggyResponse = "Dobrý den, vlak, kterým jsem jel, měl nehodu, je tu spousta zraněných!";

    [Header("Triggers")]
    [SerializeField] string openTrigger = "Open";
    [SerializeField] string closeTrigger = "Close";

    Animator speechBubbleAnimator, speechBubbleDoggyAnimator;
    Animator speakingIconAnimator;
    TextMeshProUGUI speechBubbleText, speechBubbleDoggyText;

    void Awake()
    {
        speechBubbleAnimator = speechBubbleOneLine.GetComponent<Animator>();
        speechBubbleDoggyAnimator = speechBubbleDoggy.GetComponent<Animator>();
        speakingIconAnimator = speakingIcon.GetComponent<Animator>();
        speechBubbleText = speechBubbleOneLine.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        speechBubbleDoggyText = speechBubbleDoggy.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    internal IEnumerator StartCall155(bool call155) {

        taskManager.ResetBubbleText(speechBubbleText);
        taskManager.soundManagement.PlayAudioSource(doggyCorrect);

        yield return new WaitForSeconds(6);

        speakingIcon.SetActive(true);
        speechBubbleAnimator.SetTrigger(openTrigger);

        yield return new WaitForSeconds(2);

        // Create introduce
        string introduce = introducePart1;
        if (call155) {
            introduce += introduce155;
            taskManager.soundManagement.PlayOperatorAudio("1-155");
        } else {
            introduce += introduce112;
            taskManager.soundManagement.PlayOperatorAudio("1-112");
        }
        introduce += introducePart2;

        StartCoroutine(taskManager.WriteText(speechBubbleText, introduce));


        // Doggy describes the situation
        yield return new WaitForSeconds(4);

        taskManager.ResetBubbleText(speechBubbleDoggyText);
        speechBubbleDoggyAnimator.SetTrigger(openTrigger);
        yield return new WaitForSeconds(1);

        StartCoroutine(taskManager.WriteText(speechBubbleDoggyText, doggyResponse));
        yield return new WaitForSeconds(6);
        taskManager.ResetBubbleText(speechBubbleDoggyText);
        yield return new WaitForSeconds(0.2f);
        speechBubbleDoggyAnimator.SetTrigger(closeTrigger);

        // Answer
        StartCoroutine(taskManager.WriteText(speechBubbleText, afterCorrectAnswer));
        taskManager.soundManagement.PlayOperatorAudio("2");
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
