using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject[] notifMessages;

    [Header("Battery Check")]
    public ItemManager itemManager;
    public bool rechargedMessageSent;

    void Start()
    {
        XrayAllowed = true; //To make sure that the boolean is set right at start of game.
        XrayState = false; //To make sure X-Ray is off at start of game.
        rechargedMessageSent = false; //Making sure this is false at start of game.

        notifBackground.SetActive(false); //Make sure notification background is inactive at start
        foreach (GameObject notifMessage in notifMessages) //Make sure each notification message is inactive at start
        {
            notifMessage.SetActive(false); 
        }
    }
    
    //Dit is de functie die de XrayState boolean flipped wanneer input wordt waargenomen.
    public void OnXrayToggle(InputAction.CallbackContext context)
    {
        if (XrayAllowed == true) //Checks if the Xray is allowed to be use, this is true before finding Door Key & after finding Battery.
        {
            //Als dit niet de eerste keer is dat je signaal binnenkrijgt dan stopt ie meteen.
            //Met 1 key input geef je vaak meer dan 1 input signal. 
            //Om ervoor te zorgen dat je maar 1x de xray toggled stopt die als het niet de eerste input is.
            if (!context.started)
                return;
            //Dit flipped de boolean.
            XrayState = !XrayState;

            
            

            //Toggled de xray aan/uit.
            ActivateXray();
        }
        else
        {
            Debug.Log("X-Ray ILLEGAL ILLEGAL ILLAGEL"); //To check if it's indeed turned off.
            notifMessages[2].SetActive(true);
        }
        ShowNotification();
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
        notifBackground.SetActive(true); //Background active

        if (XrayState == true)
        {
            notifMessages[1].SetActive(false); //If X-Ray is on, remove message
            notifMessages[0].SetActive(true); //If X-Ray is on, show message
            notifMessages[3].SetActive(false); //If X-Ray is off, remove message
        }
        if (XrayState == false)
        {
            notifMessages[0].SetActive(false); //If X-Ray is off, remove message
            notifMessages[1].SetActive(true); //If X-Ray is off, show message
        }
        if (XrayAllowed == false)
        {
            notifMessages[2].SetActive(true); //If X-Ray is disabled, show message "Battery empty"
            Debug.Log("JUST CHECKING SMTH DONT MIND ME");
            notifMessages[0].SetActive(false); //If X-Ray is disabled, remove message "X-Ray On"
            notifMessages[1].SetActive(false); //If X-Ray is disabled, remove message "X-Ray Off"
        }
        if (itemManager.BatteryFound == true)
        {
            notifMessages[3].SetActive(false); //If Battery was found, remove message on input.
        }
    }

}
