using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class DialogueUI : MonoBehaviour
{
    //References naar gameobjecten/inputs zodat het bruikbaar is in het script
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject Interactable;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private InputActionReference nextDialogueAction;
    [SerializeField] public GameObject dialogueTrigger;

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;


    // Bij het opstarten zorgt dit ervoor dat hij de script van de typewriter effect en responsehandler ophaalt en maakt de dialogebox leeg
    private void Start()
    {
        //haalt de typewriter effect op
        typewriterEffect = GetComponent<TypewriterEffect>();
        //haalt de responses op
        responseHandler = GetComponent<ResponseHandler>();

        //sluit de dialogue canvas
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        Interactable.SetActive(false);
        //zorgt dat er niet al iets staat in de dialogue en refreshed de dialogue zo wel
        StopAllCoroutines();
        //Vertoont de dialogue canvas
        dialogueBox.SetActive(true);
        //Start de dialogue context
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //Dialogue progressie houd in dat hij hier kijkt hoeveel dialogue er is, zijn er responses. Als er geen mogelijke dialogue is sluit hij de dialogue
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            //haalt de dialogue objects op
            string dialogue = dialogueObject.Dialogue[i];
            //Zorgt ervoor dat de typewriter effect wordt toegepast op de dialogue
            yield return typewriterEffect.Run(dialogue, textLabel);
            //checkt voor mogelijke responses
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            //Wacht totdat de dialogue typewrite effect is afgelopen en vertoont volgende dialogue als spat
            yield return new WaitUntil(() => nextDialogueAction.action.WasPressedThisFrame());
        }

        if (dialogueObject.HasResponses)
        {
            //vertoont de responses die zijn toegewezen
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            //sluit de dialogue
            CloseDialogueBox();
        }
    }

    public void CloseDialogueBox()
    {
        //Sluit de dialogue canvas
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}