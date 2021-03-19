using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task3Location : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;

    [Header("Speech bubble")]
    [SerializeField] TextMeshProUGUI speechBubbleText = default;


    [SerializeField] GameObject answers = default;

    [SerializeField] GameObject darkBackground = default;

    [Header("Operator's speech")]
    [TextArea]
    [SerializeField] string question = "Kde se to stalo?";
    [TextArea]
    [SerializeField] string wrongAnswer = "To nestačí, na jaké trase se to stalo";


    internal void StartTask3Location() {
        StartCoroutine(StartTask());
    }

    IEnumerator StartTask() {
        taskManager.ResetBubbleText(speechBubbleText);

        yield return new WaitForSeconds(2);

        // TODO taskText null, length check in manager

        StartCoroutine(taskManager.WriteText(speechBubbleText, question));
        
        yield return new WaitForSeconds(1);

        darkBackground.SetActive(true);
        answers.SetActive(true);
        enableButtons(false);

        yield return new WaitForSeconds(2);

        enableButtons(true);
    }

    // Wrong answer selected
    IEnumerator WrongAnswer(Button button) {

        // Disable wrong answer button
        button.interactable = false;
        button.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(158, 158, 158, 255);

        // Feedback
        StartCoroutine(taskManager.WriteText(speechBubbleText, wrongAnswer));
        enableButtons(false);
        yield return new WaitForSeconds(2);
        enableButtons(true);
    }

    // Correct answer selected
    private void CorrectAnswer() {
        answers.SetActive(false);
        darkBackground.SetActive(false);

        // Next task
        taskManager.task4WindowView.StartTask4WindowView();
    }

    // Enable/disable answer buttons
    private void enableButtons (bool state) {
        Button[] buttons = answers.gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons) {
            if (button.interactable != false) {
                button.enabled = state;
            }
        }
    }

    internal void AnswerSelected(Button button) {
        if (button.name == "Correct") {
            CorrectAnswer();
        } else {
            StartCoroutine(WrongAnswer(button));
        }
    }

}
