using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    [SerializeField] GameObject arrowRight = default;
    [SerializeField] GameObject arrowLeft = default;

    public void ShowArrowRight() {
        arrowLeft.SetActive(false);
        arrowRight.SetActive(true);
    }

    public void ShowArrowLeft() {
        arrowRight.SetActive(false);
        arrowLeft.SetActive(true);
    }
}
