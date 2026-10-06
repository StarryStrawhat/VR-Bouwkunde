using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueUI: MonoBehaviour
{
    //References naar dingen
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue; 
    [SerializeField] private InputActionReference nextDialogueAction;

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

// Bepaald wat er in de textbox komt te staan
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }   

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //Dialogue progressie zoals next dialogue
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
    private void CloseDialogueBox()
    { 
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
