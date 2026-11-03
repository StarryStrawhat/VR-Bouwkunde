using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    //References naar andere gameobjecten/script/inputs zodat je ze kunt gebruiken in je script
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogue;
    [SerializeField] private InputActionReference InteractAction;
    //Boolean die checkt of de speler in of uit het veld zit
    bool player_detection = false;
    //Boolean die checkt of de speler geïnteract heeft met de NPC
    bool hasInteracted = false;

   
    void Update()
    {
        // Checkt of de player in het veld van de dialogue zit en E wordt ingedrukt
        if (player_detection && !hasInteracted && InteractAction.action.WasPressedThisFrame())
        {
            //Regelt dat er met de NPC is geïnteract
            hasInteracted = true;
            //Vertoont de dialogue
            dialogueUI.ShowDialogue(dialogue);
        }
    }

    //valideert dat de speler in het veld van de dialogue zit
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checking...");
        if (other.CompareTag("Player")) 
        {
            player_detection = true;
            Debug.Log("Player detected!!!");
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