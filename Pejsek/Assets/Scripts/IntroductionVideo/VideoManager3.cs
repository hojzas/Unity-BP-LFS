using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager3 : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    
    [SerializeField] Animator explosionAnimator = default;
    [SerializeField] GameObject BlackImage = default;

    [SerializeField] GameObject stones = default;
    [SerializeField] GameObject cliff = default;
    [SerializeField] GameObject heap = default;

    [SerializeField] string startTrigger = "Start";

    [SerializeField] float stoneDelay = 0.5f;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(soundManager.VolumeUp(0.01f, 0.6f));

        soundManager.PlayAudio("landslide");
        soundManager.PlayAudio("digging");
        StartCoroutine(StartStoneFalling());
    }

    IEnumerator StartStoneFalling() {

        for (int i = 0; i < stones.transform.childCount; i++) {
            SetStoneActive(i);

            stoneDelay -= i / 10;

            if (stoneDelay < 0.3f) stoneDelay = 0.3f;

            yield return new WaitForSeconds(stoneDelay);
        }
        
    }

    void SetStoneActive(int id) {
        stones.transform.GetChild(id).gameObject.SetActive(true);
    }

    void Explode() {
        explosionAnimator.SetTrigger(startTrigger);
        soundManager.PlayAudio("cliff");
    }

    void ShowHeap() {
        stones.SetActive(false);
        cliff.SetActive(false);
        heap.SetActive(true);
    }


    void TrainArrival() {
        StartCoroutine(soundManager.VolumeUp(0.004f, 0.3f));

        soundManager.PlayAudio("trainCrash");
        soundManager.PlayAudio("trainPreCrash");
    }


    IEnumerator TrainCrash() {
        BlackImage.SetActive(true);

        yield return new WaitForSeconds(4);

        StartCoroutine(soundManager.VolumeDown(0.0025f));
        
        yield return new WaitForSeconds(3);

        NextScene();
    }

    // Load next introduction scene
    void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
