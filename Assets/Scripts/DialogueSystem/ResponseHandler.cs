using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;

public class ResponseHandler : MonoBehaviour
{
    //Reference naar de ResponseBox waarin in de responses staan zodat je dit in je code kunt gebruiken
    [SerializeField] private RectTransform responseBox;
    //Reference naar de Templateantwoord zodat je dit kunt gebruiken in je code
    [SerializeField] private RectTransform responseButtonTemplate;
    //Reference naar de responsesContainer zodat je deze in je code kunt gebruiken
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    //maakt een tijdelijke lijst aan om het later beter te organiseren
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        //haalt de dialogue ui script op
        dialogueUI = GetComponent<DialogueUI>(); 
    }

    // Maakt voor elke response een button aan en in de responsebox
    public void ShowResponses(Response[] responses)
    {
        //geeft de hoogte aan van de responses in het algemeen
        float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            //creeërt een kopie van de template in de container
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            //zorgt dat de response buttons bruikbaar zijn
            responseButton.gameObject.SetActive(true);
            //Zet de zelfgemaakte responses van de dialogue objecten in de text
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            //Haalt het antwoord van de gekozen response op
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            //voegt de template toe in de response box
            tempResponseButtons.Add(responseButton);

            //Telt de hoogte van de response box gebaseerd op de hoeveelheid aan responses die je hebt bij elkaar
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        //past de hoogte van de responsebox aan op de hoeveelheid responses
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        //zorgt dat de responses zichtbaar zijn 
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        //verstopt de responses bij het aanklikken van een response
        responseBox.gameObject.SetActive(false);

        //verwijdert de responses weg na gebruik
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        //maakt de lijst weer leeg voor andere keuzes
        tempResponseButtons.Clear();

        //laat de dialogue zien van de gekozen response
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
