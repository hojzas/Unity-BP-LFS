using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    [SerializeField] PlayerController playerController = default;

    [SerializeField] float scaleDivergence =  0.05f;

    internal float scale;
    float scaleValue;

    // Start is called before the first frame update
    void Start()
    {
        // Get scale value so object could resize based on distance
        scaleValue = transform.localScale.y / -transform.position.y;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Scale object based on distance, (y axis) + divergence so object is not so small in the back
        if (playerController.playerMovement.IsMoving()) {
            scale = -transform.position.y * scaleValue + (transform.position.y + 1 / scaleValue) * scaleDivergence;

            transform.localScale = new Vector2(scale, scale);
        }
    }
}
