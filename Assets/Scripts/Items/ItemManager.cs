using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public XrayManager xrayManager;

    [Header("Items Found")]
    public bool DoorKeyFound = false;
    public bool EmergencyKeyFound = false;
    public bool SledgehammerFound = false;
    public bool BatteryFound = false;
    public bool DoorHandleFound = false;
    public bool ScrewdriverFound = false;

    [Header("Items")]
    public GameObject DoorKey;
    public GameObject EmergencyKey;
    public GameObject Sledgehammer;
    public GameObject Battery;
    public GameObject DoorHandle;
    public GameObject Screwdriver;

    void Start()
    {
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

    public void GrabDoorKey()
    {
        if (DoorKeyFound == true)
        {
            DoorKey.SetActive(false);
        }
    }

    public void GrabBattery()
    {
        if (BatteryFound == true)
        {
            Battery.SetActive(false);
        }
    }

    public void FlipXrayAllow()
    {
        Debug.Log("Flipping XrayAllow...");
        xrayManager.XrayAllowed = !xrayManager.XrayAllowed;
        if (xrayManager.XrayState == true)
        {
            xrayManager.XrayState = false;
            xrayManager.SwapMaterial();
        }
        Debug.Log("XrayAllow FLIPPED!!!");
    }

}
