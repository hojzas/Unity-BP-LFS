using System.Collections;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Other tasks scripts")]
    [SerializeField] internal Task1TakeMobile task1TakeMobile = default;
    [SerializeField] internal Task2OperatorCallStart task2OperatorCallStart = default;
    [SerializeField] internal Task3Location task3Location = default;
    [SerializeField] internal Task4WindowView task4WindowView = default;
    [SerializeField] internal Task5AppZachranka task5AppZachranka = default;
    [SerializeField] internal Task6TrainSearch task6TrainSearch = default;

    [SerializeField] internal MoveLandscape moveLandscape = default;

    [SerializeField] internal SoundManagement soundManagement = default;
    [SerializeField] internal InteractiveObject interactiveObject = default;




    [Header("Speed of writing text into a speech bubble")]
    [SerializeField] float speed = 0.05f;

    // Begin with 1st task
    void Start()
    {
        if (task6TrainSearch.isFirstTask()) {
            // Run task 1
            task1TakeMobile.StartTask1TakeMobile();
        } else {
            // Task6 continue in other wagon
            task6TrainSearch.StartTask6TrainSearch();
        }
    }

    // Fill up speech bubble with text
    internal IEnumerator WriteText(TextMeshProUGUI speechBubble, string text) {
        speechBubble.text = "";
        
        foreach (char c in text) {
            speechBubble.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    // Reset speech bubble text
    internal void ResetBubbleText(TextMeshProUGUI speechBubble) {
        speechBubble.text = "";
    }
}
