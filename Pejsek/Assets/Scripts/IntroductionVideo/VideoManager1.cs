using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager1 : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;

    [SerializeField] Animator latecomerAnimator = default;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(soundManager.VolumeUp(0.01f, 1));
        
        soundManager.PlayAudio("trainDepart");
        soundManager.stationNoise.Play();
    }

    // Load next introduction scene
    void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Play sounds, called by animation events
    void OpenDoor() {
        soundManager.PlayAudio("trainDoorOpening");
    }

    void StartBraking() {
        soundManager.PlayAudio("trainBraking");
    }

    void LatecomerStartRun() {
        latecomerAnimator.SetTrigger("Start");
    }

    void VolumeDown() {
        StartCoroutine(soundManager.VolumeDown(0.0018f));
    }

    void StationNoiseStop() {
        soundManager.stationNoise.Stop();
    }


}
