﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task3Location : MonoBehaviour
{
    [SerializeField] internal TaskManager taskManager = default;

    [Header("Sounds")]
    [SerializeField] AudioSource wrongAnswerSound = default;
    [SerializeField] AudioSource correctAnswerSound = default;

    [Header("Speech bubble")]
    [SerializeField] TextMeshProUGUI speechBubbleText = default;


    [SerializeField] GameObject answers = default;

    [SerializeField] GameObject darkBackground = default;

    [Header("Operator's speech")]
    [TextArea]
    [SerializeField] string question = "Kde se to stalo?";
    [TextArea]
    string wrongAnswer = "To nestačí, na jaké trase se to stalo?" + System.Environment.NewLine + "Kde jste nastupovali a kam jste jeli?";


    internal void StartTask3Location() {
        StartCoroutine(StartTask());
    }

    IEnumerator StartTask() {
        taskManager.ResetBubbleText(speechBubbleText);

        yield return new WaitForSeconds(2);

        StartCoroutine(taskManager.WriteText(speechBubbleText, question));
        taskManager.soundManagement.PlayOperatorAudio("3");
        
        yield return new WaitForSeconds(1);

        darkBackground.SetActive(true);
        answers.SetActive(true);
        enableButtons(false);
        taskManager.interactiveObject.SetInteractiveObjectClickable(false);

        yield return new WaitForSeconds(2);

        enableButtons(true);
    }

    // Wrong answer selected
    IEnumerator WrongAnswer(Button button) {

        // Disable wrong answer button
        taskManager.soundManagement.PlayAudioSource(wrongAnswerSound);
        button.interactable = false;
        button.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(158, 158, 158, 255);

        // Feedback
        StartCoroutine(taskManager.WriteText(speechBubbleText, wrongAnswer));
        taskManager.soundManagement.PlayOperatorAudio("3-wrong");
        enableButtons(false);
        yield return new WaitForSeconds(4);
        enableButtons(true);
    }

    // Correct answer selected
    private IEnumerator CorrectAnswer() {
        enableButtons(false);
        taskManager.soundManagement.PlayAudioSource(correctAnswerSound);
        yield return new WaitForSeconds(0.5f);
        answers.SetActive(false);
        darkBackground.SetActive(false);

        // Next task
        taskManager.interactiveObject.SetInteractiveObjectClickable(true);
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
            StartCoroutine(CorrectAnswer());
        } else {
            StartCoroutine(WrongAnswer(button));
        }
    }

}
