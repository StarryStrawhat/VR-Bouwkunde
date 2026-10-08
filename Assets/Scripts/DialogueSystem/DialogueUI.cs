using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class DialogueUI: MonoBehaviour
{
    //References naar dingen
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private InputActionReference nextDialogueAction;

    [SerializeField] public GameObject dialogueTrigger;

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private bool isDialogueRunning;


    // Bepaald wat er in de textbox komt te staan
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StopAllCoroutines();

        isDialogueRunning = (true);
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
        isDialogueRunning = false;
    }
}
