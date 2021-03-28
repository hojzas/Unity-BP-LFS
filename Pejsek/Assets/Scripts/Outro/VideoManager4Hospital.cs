using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager4Hospital : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;

    [Header("Helicopter")]
    [SerializeField] GameObject helicopter = default;
    [SerializeField] GameObject helicopterFlying = default;
    [SerializeField] Animator propellerAnimator = default;

    [SerializeField] Animator blackTransitionAnimator = default;

    void BirdSing() {
        soundManager.PlayAudio("birdSing");
    }

    void Fly() {
        soundManager.SetVolumeValue(1);
        helicopter.SetActive(false);
        helicopterFlying.SetActive(true);
        soundManager.PlayAudio("helicopterTakeOff");
        propellerAnimator.SetTrigger("Fly");
    }

    void HelicopterFadeAway() {
        StartCoroutine(soundManager.VolumeDown(0.005f));
    }

    // Load next outro scene
    IEnumerator NextScene() {
        blackTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
