using System;
using System.Collections;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class XrayManager : MonoBehaviour
{
    [Header("X-Ray Booleans")] 
    public bool XrayState = false; //Boolean that controls if X-ray is on or off.
    public bool XrayAllowed = true; //Boolean that controls if the Xray is allowed to be used or not.

    [Header("X-Ray 2.0")] //Array with Objects that swap material.
    public GameObject[] TestCubes;
    //De 2 Materialen waar het object tussen wisselt.
    public Material XrayMaterial;
    public Material VisibleMaterial;

    [Header("Notifications")]
    public GameObject notifBackground;
    public Color32 notifBackgroundColor;
    public TextMeshProUGUI[] notifMessages;
    public Color32 notifMessagesColor;
    public Color32 notifMessagesInvis;

    [Header("Battery Check")]
    public ItemManager itemManager;
    public bool rechargedMessageSent;

    void Start()
    {
        XrayAllowed = true; //To make sure that the boolean is set right at start of game.
        XrayState = false; //To make sure X-Ray is off at start of game.
        rechargedMessageSent = false; //Making sure this is false at start of game.
        notifBackgroundColor.a = 0; //Setting alpha to 0 so background is invisible.
        notifBackground.GetComponent<Image>().color = notifBackgroundColor; //Make sure background of notification is right at start.
        notifMessagesColor.a = 0; //Setting alpha to 0 so messages are invisible.

        foreach (TextMeshProUGUI notifMessage in notifMessages)
        {
            notifMessage.color = notifMessagesColor; //Make sure each notification message is invisible at start.
        }
    }

    private void Update()
    {
        notifBackground.GetComponent<Image>().color = notifBackgroundColor; //Constantly set the color of the background image to notifBackgroundColor so the alpha is updated.

        if (XrayState == true)
        {
            notifMessages[1].color = notifMessagesInvis;
            notifMessages[0].color = notifMessagesColor; //If X-Ray is on, constantly set text color to notifBackgroundText so alpha is updated 
        }
        if (XrayState == false)
        {
            notifMessages[0].color = notifMessagesInvis;
            notifMessages[1].color = notifMessagesColor; //If X-Ray is off, constantly set text color to notifBackgroundText so alpha is updated
        }
        if (XrayAllowed == false)
        {
            notifMessages[2].color = notifMessagesColor; //If X-Ray is not allowed, constantly set text color to notifBackgroundText so alpha is updated
        }
        if (itemManager.BatteryFound == true)
        {
            notifMessages[0].color = notifMessagesColor; //If X-Ray is on, constantly set text color to notifBackgroundText so alpha is updated
        }

        if (notifBackgroundColor.a <= 0)
        {
            StopCoroutine(FadeNotificationBackground());
            Debug.Log("Stopped fade Background");
        }
        if (notifMessagesColor.a <= 0)
        {
            StopCoroutine(FadeMessages());
            Debug.Log("Stopped fade Text");
        }

    }

    //This is the function that flips the XrayState boolean when an input is detected.
    public void OnXrayToggle(InputAction.CallbackContext context)
    {
        if (XrayAllowed == true) //Checks if the Xray is allowed to be use, this is true before finding Door Key & after finding Battery.
        {
            //If this isn't the first time you get a signal, stop immidiately. With 1 key input you send more than 1 signal.
            //To make sure you toggle the xray once, it stops if it's not the first signal from the input.
            if (!context.started)
                return;
            
            XrayState = !XrayState; //Flips the boolean

            ActivateXray(); //Toggles the xray on/off
        }
        else //If xray isn't allowed:
        {
            notifMessages[2].GetComponent<Text>().color = notifMessagesColor; //Set message of empty batteries active.
        }
        ShowNotification(); //Shows the right notification depending on XrayAllow & XrayState.

        StartCoroutine(FadeMessages()); //Start to fade out notification text
        StartCoroutine(FadeNotificationBackground()); //Start to fade out notification background


    }

    IEnumerator FadeNotificationBackground() //Called in a coroutine so it happens alongside everything else.
    {
        for (notifBackgroundColor.a = 255; notifBackgroundColor.a >= 0; notifBackgroundColor.a -= 5) //If the alpha is 1f, it will decrease by 0.1f until it hits 0.
        {
            if (notifBackgroundColor.a > 250) //If alpha is greater than 0.98, wait longer before looping, this is to make sure the notification stays for a few seconds.
            {
                Debug.Log("Background:" + notifBackgroundColor.a); //Checking current value.
                yield return new WaitForSecondsRealtime(3f); //Wait 3 seconds before looping
            }
            else
            {
                Debug.Log("Background:" + notifBackgroundColor.a); //Checking current value.
                yield return new WaitForSecondsRealtime(0.1f); //Wait 0.5 seconds before looping.
            }
                
        }
    }

    IEnumerator FadeMessages()
    {
        for (notifMessagesColor.a = 255; notifMessagesColor.a >= 0; notifMessagesColor.a -= 5)
        {
            if (notifMessagesColor.a > 250)
            {
                Debug.Log("Messages:" + notifBackgroundColor.a); //Checking current value.
                yield return new WaitForSecondsRealtime(3f);
            }
            else
            {
                Debug.Log("Messages:" + notifBackgroundColor.a); //Checking current value.
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }

    public void ActivateXray()
    {
        //X-Ray 2.0: 1 Object, wisselt het materiaal van het Object om zodat die doorzichtig wordt.
        //Wisselt het materiaal om.
        SwapMaterial();

    }

    //X-Ray 2.0
    //Checks XrayState, if true, zet het materiaal van geselecteerde Object naar
    // materiaal A, if false, zet materiaal naar materiaal B.
    public void SwapMaterial()
    {
        if (XrayState == true)
        {
            //Kijkt voor elk Object in TestCube Array naar MeshRenderer component, veranderd het materiaal voor elk Object.
            foreach (GameObject TestCube in TestCubes)
            {
                TestCube.GetComponent<MeshRenderer>().material = XrayMaterial;
            }
        }
        else
        {
            //Kijkt voor elk Object in TestCube Array naar MeshRenderer component, veranderd het materiaal voor elk Object.
            foreach (GameObject TestCube in TestCubes)
            {
                TestCube.GetComponent<MeshRenderer>().material = VisibleMaterial;
            }
        }
    }

    public void ShowNotification() //Sets the notification background & messages active
    {
        //notifBackground.SetActive(true); //Background active
        notifBackgroundColor.a = 255; //Background is visible
        notifMessagesColor.a = 255;
        Debug.Log("Background filled" + notifBackgroundColor.a);
        Debug.Log("Messages filled" + notifMessagesColor.a);

        //if (XrayState == true)
        //{
        //    notifMessages[1].SetActive(false); //If X-Ray is on, remove message
        //    notifMessages[0].SetActive(true); //If X-Ray is on, show message
        //    notifMessages[3].SetActive(false); //If X-Ray is off, remove message
        //}
        //if (XrayState == false)
        //{
        //    notifMessages[0].SetActive(false); //If X-Ray is off, remove message
        //    notifMessages[1].SetActive(true); //If X-Ray is off, show message
        //}
        //if (XrayAllowed == false)
        //{
        //    notifMessages[2].SetActive(true); //If X-Ray is disabled, show message "Battery empty"
        //    Debug.Log("JUST CHECKING SMTH DONT MIND ME");
        //    notifMessages[0].SetActive(false); //If X-Ray is disabled, remove message "X-Ray On"
        //    notifMessages[1].SetActive(false); //If X-Ray is disabled, remove message "X-Ray Off"
        //}
        //if (itemManager.BatteryFound == true)
        //{
        //    notifMessages[3].SetActive(false); //If Battery was found, remove message on input.
        //}
    }

}
