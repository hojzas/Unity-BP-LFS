using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;

    [SerializeField] Animator latecomerAnimator = default;

    // Start is called before the first frame update
    void Start() {
        soundManager.PlayAudio("trainDepart");
    }

    // Load next introduction scene
    void NextScene() {
        soundManager.StopAudio();
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


}
