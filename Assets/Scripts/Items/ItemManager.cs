using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public XrayManager xrayManager;

    [Header("Items Found")]
    public bool DoorKeyFound = false;
    public bool EmergencyKeyFound = false;
    public bool SledgehammerFound = false;
    public bool DoorHandleFound = false;
    public bool ScrewdriverFound = false;

    [Header("Items")]
    public GameObject DoorKeyTest;

    void Start()
    {
        DoorKeyFound = false;
        EmergencyKeyFound = false;
        SledgehammerFound = false;
        DoorHandleFound = false;
        ScrewdriverFound = false;

    }

    public void Update()
    {
        
    }

    public void GrabDoorKey()
    {
        if (DoorKeyFound == true)
        {
            xrayManager.XrayAllowed = false;
        }
    }

    public void FlipXrayAllow()
    {
        Debug.Log("Flipping XrayAllow...");
        xrayManager.XrayAllowed = !xrayManager.XrayAllowed;
        Debug.Log("XrayAllow FLIPPED!!!");
    }

}
