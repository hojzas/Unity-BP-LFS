using System.Collections;
using UnityEngine;

public class VideoManager2TrainPassingBy : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] SceneController sceneController = default;
    [SerializeField] Animator blackTransitionAnimator = default;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(soundManager.VolumeUp(0.01f, 0.8f));
        soundManager.PlayAudio("trainPassingBy");
    }

    void VolumeDown() {
        StartCoroutine(soundManager.VolumeDown(0.005f));
        StartCoroutine(NextScene());
    }

    // Load next introduction scene
    IEnumerator NextScene() {
        blackTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        sceneController.LoadNextScene();
    }
}
