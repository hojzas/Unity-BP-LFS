using UnityEngine;

public class VideoManager1Station : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] SceneController sceneController = default;

    [SerializeField] Animator latecomerAnimator = default;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(soundManager.VolumeUp(0.01f, 1));
        
        soundManager.PlayAudio("trainDepart");
        soundManager.PlayStationNoise();
    }

    // Load next introduction scene
    void NextScene() {
        StartCoroutine(sceneController.LoadNextScene(false));
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
