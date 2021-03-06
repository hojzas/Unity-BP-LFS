using System.Collections;
using UnityEngine;
using TMPro;

public class MobileButtonsControl : MonoBehaviour
{
    [SerializeField] SoundManagement soundManagement = default;
    [SerializeField] internal TaskManager taskManager = default;
    [SerializeField] TMP_Text mobileText = default;
    [SerializeField] GameObject doggy = default;
    [SerializeField] GameObject doggyHead = default;
    [SerializeField] Animator MobileTextAnimator = default;

    [Header("Speech bubble")]
    [SerializeField] GameObject speechBubble = default;
    [TextArea]
    [SerializeField] string doggyCall155Text = "Vytoč číslo záchranné služby!";

    [Header("Sounds")]
    [SerializeField] AudioSource audioButton = default;
    [SerializeField] AudioSource audioWrongNumber = default;
    [SerializeField] AudioSource audioDoggyCall155 = default;
    [SerializeField] AudioSource audioDoggyTryAgain = default;

    bool buttonsEnable = true;

    Animator doggyAnimator, mobileAnimator, speechBubbleAnimator;
    TextMeshProUGUI speechBubbleText;
    AudioSource audioCall;

    void Start() {
        doggyAnimator = doggy.GetComponent<Animator>();
        mobileAnimator = transform.Find("MobileKeyboard").gameObject.GetComponent<Animator>();
        audioCall = gameObject.GetComponent<AudioSource>();

        // Speech bubble
        speechBubbleAnimator = speechBubble.GetComponent<Animator>();
        speechBubbleText = speechBubble.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Setter enable buttons
    void EnableButtons() {
        buttonsEnable = true;
    }

    // Setter disable buttons
    void DisableButtons() {
        buttonsEnable = false;
    }

    // Getter
    bool ButtonsEnable() {
        return buttonsEnable;
    }

    internal void DisplaySpeechBubble() {
        StartCoroutine(DisplaySpeechBubbleDelay());
    }

    // Display doggy speech bubble text
    IEnumerator DisplaySpeechBubbleDelay() {
        // Open
        yield return new WaitForSeconds(0.5f);
        taskManager.ResetBubbleText(speechBubbleText);
        speechBubbleAnimator.SetTrigger("Open");

        // Write text
        yield return new WaitForSeconds(1);
        StartCoroutine(taskManager.WriteText(speechBubbleText, doggyCall155Text));
        soundManagement.PlayAudioSource(audioDoggyCall155);
    }

    // Display clicked button number
    public void DisplayNumber(string number) {
        if (mobileText.text.Length < 9 && ButtonsEnable()) {
            soundManagement.PlayAudioSource(audioButton);
            mobileText.text += number;
        }
    }

    // Delete last number
    public void Backspace() {
        if(mobileText.text.Length > 0 && ButtonsEnable()) {
            soundManagement.PlayAudioSource(audioButton);
            mobileText.text = mobileText.text.Remove(mobileText.text.Length - 1, 1);
        }
    }

    // Check corect number and call
    public void Call() {

        if (buttonsEnable) {

            soundManagement.PlayAudioSource(audioButton);

            // Acceptable numbers are 155 and 112
            if(mobileText.text == "155" || mobileText.text == "112") {
                // Correct, call
                DisableButtons();
                mobileAnimator.Play("Phone_call");

                audioDoggyCall155.Stop();
                soundManagement.PlayAudioSource(audioCall);
                doggyHead.SetActive(false);

                // Close speech bubble
                taskManager.ResetBubbleText(speechBubbleText);
                speechBubbleAnimator.SetTrigger("Close");

                // Doggy feedback
                doggyAnimator.ResetTrigger("Wrong");
                doggyAnimator.ResetTrigger("Normal");
                doggyAnimator.SetTrigger("Correct");

                StartCoroutine(HideMobile());

                bool call155;

                // Recognition of numbers for later introduction of the operator
                if (mobileText.text == "155") {
                    call155 = true;
                } else {
                    call155 = false;
                }

                // Start next task
                StartCoroutine(taskManager.task2OperatorCallStart.StartCall155(call155));

            } else {
                // Wrong, feedback
                
                StartCoroutine(WrongNumber());

                // Play wrong answer animation
                doggyAnimator.ResetTrigger("Normal");
                doggyAnimator.SetTrigger("Wrong");
            }
        }
    }

    // Wait few seconds to connect call, than hide mobile
    public IEnumerator HideMobile() {
        yield return new WaitForSeconds(6);
        mobileAnimator.Play("MobileScreenZoomOut");
        yield return new WaitForSeconds(1);
        transform.Find("MobileKeyboard").gameObject.SetActive(false);
        audioCall.Stop();
    }

    // Wrong number feedback
    IEnumerator WrongNumber() {
        soundManagement.PlayAudioSource(audioWrongNumber);
        DisableButtons();
        MobileTextAnimator.SetTrigger("Flash");

        yield return new WaitForSeconds(1);
        soundManagement.PlayAudioSource(audioDoggyTryAgain);
        mobileText.text = "";
        EnableButtons();
    }
}
