using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogue;
    [SerializeField] private InputActionReference InteractAction;
    // [SerializeField] private GameObject containerGameObject;
    // [SerializeField] private DialogueInteract dialogueInteract;
    bool player_detection = false;

    // Als de player in het veld van de dialogue zit en E wordt ingedrukt, dan start de dialogue
    void Update()
    {
        if (player_detection && InteractAction.action.WasPressedThisFrame())
        {
            dialogueUI.ShowDialogue(dialogue);
            // dialogueInteract.CloseInteractText();
        }
    }

    //valideert dat de speler in het veld van de dialogue zit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            player_detection = true;
            // dialogueInteract.ShowInteractText();
        }
    }

    // valideert dat de spelder niet in het veld van de dialogue zit
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = false;
            dialogueUI.CloseDialogueBox();
            // dialogueInteract.CloseInteractText();
        }
    }
}