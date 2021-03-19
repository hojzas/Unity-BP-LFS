using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 targetPosition;
    bool move = false;
    internal void MoveToLeftWindow() {
        targetPosition = transform.position;
        move = true;
    }

    void FixedUpdate() {
        if (move && transform.position.x > -3f) {
            targetPosition.x = targetPosition.x - 0.1f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }
}
