using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public XrayManager xrayManager;

    [Header("Items Found")] //List of all items, these booleans are true once they've been found.
    public bool DoorKeyFound = false;
    public bool EmergencyKeyFound = false;
    public bool SledgehammerFound = false;
    public bool BatteryFound = false;
    public bool DoorHandleFound = false;
    public bool ScrewdriverFound = false;

    [Header("Items")] //List to reference the GameObject of each item so they can be set inactive once found.
    public GameObject DoorKey;
    public GameObject EmergencyKey;
    public GameObject Sledgehammer;
    public GameObject Battery;
    public GameObject DoorHandle;
    public GameObject Screwdriver;

    void Start()
    {
        //Making sure each boolean is set to false at start of game
        DoorKeyFound = false;
        EmergencyKeyFound = false;
        SledgehammerFound = false;
        BatteryFound = false;
        DoorHandleFound = false;
        ScrewdriverFound = false;

    }

    public void Update()
    {
        
    }

    public void GrabDoorKey() //Gets referenced in DetectMouseClickOnObject.cs
    {
        if (DoorKeyFound == true) //If DoorKey has been found
        {
            DoorKey.SetActive(false); //Set the GameObject inactive
        }
    }

    public void GrabEmergencyKey() //Gets referenced in DetectMouseClickOnObject.cs IS NOT REFERENCED YET, ITEM DOES NOT EXIST
    {
        if (EmergencyKeyFound == true) //If Emergency Key has been found
        {
            EmergencyKey.SetActive(false); //Set the GameObject inactive
        }
    }

    public void GrabSledgehammer() //Gets referenced in DetectMouseClickOnObject.cs IS NOT REFERENCED YET, ITEM DOES NOT EXIST
    {
        if (SledgehammerFound == true) //If Sledgehammer has been found
        {
            Sledgehammer.SetActive(false); //Set the GameObject inactive
        }
    }

    public void GrabBattery() //Gets referenced in DetectMouseClickOnObject.cs
    {
        if (BatteryFound == true) //If Battery has been found
        {
            Battery.SetActive(false); //Set the GameObject inactive
        }
    }
    public void GrabDoorHandle() //Gets referenced in DetectMouseClickOnObject.cs IS NOT REFERENCED YET, ITEM DOES NOT EXIST
    {
        if (DoorHandleFound == true) //If Door Handle has been found
        {
            DoorHandle.SetActive(false); //Set the GameObject inactive
        }
    }
    public void GrabScrewdriver() //Gets referenced in DetectMouseClickOnObject.cs IS NOT REFERENCED YET, ITEM DOES NOT EXIST
    {
        if (ScrewdriverFound == true) //If Screwdriver has been found
        {
            Screwdriver.SetActive(false); //Set the GameObject inactive
        }
    }

    public void FlipXrayAllow() //Referenced in DetectMouseClickOnObject.cs, disables X-Ray when DoorKey is found, enables X-Ray when Battery is found
    {
        Debug.Log("Flipping XrayAllow..."); 
        xrayManager.XrayAllowed = !xrayManager.XrayAllowed; //Flips XrayAllowed boolean
        if (xrayManager.XrayState == true) //Checks if Xray is turned on while X-Ray gets disabled
        {
            xrayManager.XrayState = false; //Reset the boolean, for some reason this doesn't turn X-Ray off
            xrayManager.SwapMaterial(); //So this is added to actually turn it off
        }
        Debug.Log("XrayAllow FLIPPED!!!");
    }

}
