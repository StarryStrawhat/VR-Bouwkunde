using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        //haalt de dialogue ui script op
        dialogueUI = GetComponent<DialogueUI>(); 
    }
    public void ShowResponses(Response[] responses)
    {
        //geeft de hoogte aan van de responses in het algemeen
        float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            //recalls event callback bij aanklikken van knop
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            //voegt de template toe in de response box
            tempResponseButtons.Add(responseButton);

            //veranderd de hoogte van de response box gebaseerd op de template
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        //verstopt de responses bij het aanklikken van 1
        responseBox.gameObject.SetActive(false);

        //gooit de responses weg
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        //laat de dialogue zien van de gekozen response
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
