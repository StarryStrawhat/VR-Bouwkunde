using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class DialogueUI : MonoBehaviour
{
    //References naar dingen
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private InputActionReference nextDialogueAction;

    [SerializeField] public GameObject dialogueTrigger;
    // [SerializeField] private DialogueInteract dialogueInteract;



    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private bool isDialogueRunning;


    // Bij het opstarten zorgt dit ervoor dat hij de script van de typewriter effect en responsehandler ophaalt en maakt de dialogebox leeg
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
        // dialogueInteract.CloseInteractText();
    }

    // stopt alle processen die bezig waren in de canvas bij opstart en start alles wat in de canvas staat
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StopAllCoroutines();

        isDialogueRunning = (true);
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //Dialogue progressie houd in dat hij hier kijkt hoeveel dialogue er is, zijn er responses. Als er geen mogelijke dialogue is sluit hij de dialogue
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => nextDialogueAction.action.WasPressedThisFrame());
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    // Sluit de dialogue
    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        isDialogueRunning = false;
    }
}