using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager5HelicopterLanding : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;

    [SerializeField] Animator propellerAnimator = default;
    [SerializeField] GameObject doggyJumping = default;
    [SerializeField] GameObject doggyGoodJob = default;
    [SerializeField] GameObject doggy = default;

    [SerializeField] Animator blackTransitionAnimator = default;

    // Start is called before the first frame update
    void Start()
    {
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
        StartCoroutine(soundManager.VolumeDown(0.005f));
    }

    void DoggyGoodJob() {
        StartCoroutine(soundManager.VolumeDown(0.01f));
        doggy.SetActive(false);
        doggyGoodJob.SetActive(true);
    }

    // Endgame, load main menu
    IEnumerator MainMenu() {
        yield return new WaitForSeconds(0.7f);
        blackTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
