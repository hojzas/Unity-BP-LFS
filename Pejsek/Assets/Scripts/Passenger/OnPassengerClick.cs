using UnityEngine;

public class OnPassengerClick : MonoBehaviour
{
    [SerializeField] TaskManager taskManager = default;
    [SerializeField] PauseMenu pauseMenu = default;

    bool found = false;
    Animator animator, animatorPlusEffect;
    GameObject plusEffect;
    AudioSource audioSource;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        plusEffect = transform.Find("PlusEffect").gameObject;
        animatorPlusEffect = plusEffect.gameObject.GetComponent<Animator>();
        audioSource = plusEffect.gameObject.GetComponent<AudioSource>();
    }

    // On passenger tap
    void OnMouseDown() {

        if (taskManager.task6TrainSearch.IsClickingEnable() && !pauseMenu.IsGamePaused()) {

            if (!PassengerFound()) {
                // Disable gameObject highlight after clicking on it
                animator.SetTrigger("Found");
                
                // Same for passenger's other part
                Transform otherPart = transform.Find("OtherPart");
                if (otherPart != null) {
                    otherPart.gameObject.GetComponent<Animator>().SetTrigger("Found");
                }

                // Display plus effect animation & sound
                animatorPlusEffect.SetTrigger("Found");
                taskManager.soundManagement.PlayAudioSource(audioSource);
                
                StartCoroutine(taskManager.task6TrainSearch.PassengerTap());
                PassengerWasFound();
            }
        }
    }

    // Setter passanger found
    void PassengerWasFound() {
        found = true;
    }

    // Getter passanger found
    bool PassengerFound() {
        return found;
    }
}
