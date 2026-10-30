using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogue;
    [SerializeField] private InputActionReference InteractAction;
    bool player_detection = false;
    bool hasInteracted = false;

    // Als de player in het veld van de dialogue zit en E wordt ingedrukt, dan start de dialogue
    void Update()
    {
        if (player_detection && !hasInteracted && InteractAction.action.WasPressedThisFrame())
        {
            hasInteracted = true;
            dialogueUI.ShowDialogue(dialogue);
        }
    }

    //valideert dat de speler in het veld van de dialogue zit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            player_detection = true;
        }
    }

    // valideert dat de spelder niet in het veld van de dialogue zit
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasInteracted = false;
            player_detection = false;
            hasInteracted = false;
            dialogueUI.CloseDialogueBox();
        }
    }
}