﻿using System.Collections;
using UnityEngine;

public class VideoManager3Crash : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] SceneController sceneController = default;
    
    [SerializeField] Animator explosionAnimator = default;
    [SerializeField] GameObject BlackImage = default;

    [SerializeField] GameObject stones = default;
    [SerializeField] GameObject cliff = default;
    [SerializeField] GameObject heap = default;

    [SerializeField] string startTrigger = "Start";

    [SerializeField] float stoneDelay = 0.5f;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartStoneFalling());
    }

    IEnumerator StartStoneFalling() {

        yield return new WaitForSeconds(1);

        StartCoroutine(soundManager.VolumeUp(0.01f, 0.6f));

        soundManager.PlayAudio("landslide");
        soundManager.PlayAudio("digging");

        // Speed up falling stones
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


    // Train crash, display black screen, decrease the volume
    IEnumerator TrainCrash() {
        BlackImage.SetActive(true);

        // Preload next scene
        StartCoroutine(sceneController.LoadAsyncNextScene());
        yield return new WaitForSeconds(4);

        StartCoroutine(soundManager.VolumeDown(0.0025f));
        
        yield return new WaitForSeconds(1);

        sceneController.LoadPreloadedScene();
    }
}
