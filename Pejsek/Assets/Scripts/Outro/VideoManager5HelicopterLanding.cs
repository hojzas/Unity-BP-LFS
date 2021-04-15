using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VideoManager5HelicopterLanding : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] SceneController sceneController = default;


    [SerializeField] Animator propellerAnimator = default;
    [SerializeField] GameObject doggyJumping = default;
    [SerializeField] GameObject doggyGoodJob = default;
    [SerializeField] GameObject doggy = default;
    [SerializeField] GameObject acknowledgmentPanel = default;

    Animator text1Animator, text2Animator;
    Image logos;

    // Start is called before the first frame update
    void Start()
    {
        // Get acknowledgment stuff
        text1Animator = acknowledgmentPanel.transform.Find("Text1").gameObject.GetComponent<Animator>();
        text2Animator = acknowledgmentPanel.transform.Find("Text2").gameObject.GetComponent<Animator>();
        logos = acknowledgmentPanel.transform.Find("Logos").gameObject.GetComponent<Image>();

        StartCoroutine(soundManager.VolumeUp(0.01f, 1));
        soundManager.PlayAudio("helicopterTakeOff");
        soundManager.PlayAudio("ambulance");
        propellerAnimator.SetTrigger("Fly");
    }

    void DoggyJumping() {
        doggy.SetActive(false);
        doggyJumping.SetActive(true);
    }

    void DoggyStopJumping() {
        doggyJumping.SetActive(false);
        doggy.SetActive(true);
    }

    void DoggyGoodJob() {
        doggy.SetActive(false);
        doggyGoodJob.SetActive(true);
    }

    // Endgame, acknowledgment, load main menu
    IEnumerator MainMenu() {

        yield return new WaitForSeconds(0.7f);
        
        // Acknowledgment
        acknowledgmentPanel.SetActive(true);
        yield return new WaitForSeconds(1);

        soundManager.StopAudio();
        soundManager.SetVolumeValue(1);
        soundManager.PlayAudio("doggyOutro");

        // Display Logos
        while (logos.fillAmount < 1) {
            logos.fillAmount += 0.01f;
            yield return null;
        }

        // Display Texts
        yield return new WaitForSeconds(1);
        text1Animator.SetTrigger("Show");

        yield return new WaitForSeconds(3);
        text2Animator.SetTrigger("Show");

        yield return new WaitForSeconds(4);

        // Load main menu
        StartCoroutine(sceneController.LoadMainMenuScene(true));
    }
}
