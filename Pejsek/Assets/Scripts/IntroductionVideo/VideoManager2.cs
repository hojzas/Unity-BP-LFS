using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager2 : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = default;
    
    [SerializeField] Animator explosionAnimator = default;

    [SerializeField] GameObject stones = default;
    [SerializeField] GameObject cliff = default;
    [SerializeField] GameObject heap = default;

    // Start is called before the first frame update
    void Start() {
        soundManager.PlayAudio("landslide");
        StartCoroutine(StartStoneFalling());
    }

    // Load next introduction scene
    void NextScene() {
        soundManager.StopAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Explode() {
        explosionAnimator.SetTrigger("Start");
    }

    IEnumerator StartStoneFalling() {
        float delay = 0.6f;

        for (int i = 0; i < stones.transform.childCount; i++) {
            SetStoneActive(i);

            delay -= i / 10;

            if (delay < 0.3f) delay = 0.3f;

            yield return new WaitForSeconds(delay);
        }
        
    }

    void SetStoneActive(int id) {
        stones.transform.GetChild(id).gameObject.SetActive(true);
    }

    void ShowHeap() {
        stones.SetActive(false);
        cliff.SetActive(false);
        heap.SetActive(true);
    }
}
