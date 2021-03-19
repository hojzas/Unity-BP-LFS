using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] internal PlayerMovement playerMovement = default;
    [SerializeField] internal PlayerCollide playerCollide = default;
    [SerializeField] internal PlayerPickUpMobile playerPickUpMobile = default;
    [SerializeField] internal TaskManager taskManager = default;


    internal bool goToNextWagon = false;

    internal bool walkEnable = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
