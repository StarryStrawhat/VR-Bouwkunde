using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    // zorgt ervoor dat hij de text haalt uit de responses
    public string ResponseText => responseText;

    // referenced naar de dialogue objects
    public DialogueObject DialogueObject => dialogueObject;
}
