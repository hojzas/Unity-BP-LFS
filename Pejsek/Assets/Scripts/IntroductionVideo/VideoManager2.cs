﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager2 : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    [SerializeField] Animator blackTransitionAnimator = default;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.VolumeUp(0.01f, 0.8f);
        soundManager.PlayAudio("trainPassingBy");
    }

    // Load next introduction scene
    void VolumeDown() {
        soundManager.VolumeDown(0.005f);
        StartCoroutine(NextScene());
    }

    // Load next introduction scene
    IEnumerator NextScene() {
        blackTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
