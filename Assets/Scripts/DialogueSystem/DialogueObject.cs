using UnityEngine;

//create een map in de assets waarin de dialogue objects in staan
[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    //Een dialogue object waarin je de intro dialogue inschrijft
    [SerializeField] [TextArea] private string[] dialogue;
    //De dialogue objects voor de responses
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}