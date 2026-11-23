using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TypewriterEffect : MonoBehaviour
{
//bepaald snelheid van de typewrite effect
    [SerializeField] private float typewriterSpeed = 30f;

    //start het type effect 
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        //geeft de coroutine terug
        return StartCoroutine(routine:TypeText(textToType, textLabel));
    }

// Verantwoordelijk voor het typ effect
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        //maakt de textbox leeg bij start
        textLabel.text = string.Empty;

        //bepaald hoe lang het duurt voordat de text verschijnt
        yield return new WaitForSeconds(1);

        //telt het aantal letters
        float t = 0;
        //Checkt de huidige positie van de letters
        int charIndex = 0;

        //While statement voor als de text bezig is met typen
        while (charIndex < textToType.Length)
        {
            //telt de verlopen tijd
            t += Time.deltaTime * typewriterSpeed;

            //zorgt ervoor dat de tel niet wordt afgerond naar kommagetallen
            charIndex = Mathf.FloorToInt(t);
            //Zorgt dat charIndex nooit kleiner dan 0 wordt en nooit groter dan de lengte van de tekst.
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            
            //zorgt ervoor dat je een klein deel van het woord zit inplaats van het hele woord
            textLabel.text = textToType.Substring(0, charIndex);
            
            //rerunt de loop bij de volgende frame
            yield return null;
        }
        //Concludeert dat alle text is uitgetypt
        textLabel.text = textToType;
    }
}
