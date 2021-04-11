using System.Collections;
using UnityEngine;

public class VideoManager4Hospital : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] SceneController sceneController = default;


    [Header("Helicopter")]
    [SerializeField] GameObject helicopter = default;
    [SerializeField] GameObject helicopterFlying = default;
    [SerializeField] Animator propellerAnimator = default;

    void BirdSing() {
        soundManager.SetVolumeValue(0.3f);
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
    void NextScene() {
        StartCoroutine(sceneController.LoadNextScene(true));

    }
}
