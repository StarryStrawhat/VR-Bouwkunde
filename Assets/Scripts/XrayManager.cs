using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class XrayManager : MonoBehaviour
{
    public bool XrayState = false;
    public GameObject[] XrayObjects;
    public GameObject[] VisibleObjects;
    void Start()
    {
        ActivateXray();
    }

    void Update()
    {
        
    }
    
    public void OnXrayToggle(InputAction.CallbackContext context)
    {
        //Als dit niet de eerste x is dat je signaal binnenkrijgt dan stopt ie meteen.
        if (!context.started)
            return;
       
        XrayState = !XrayState;

        ActivateXray();
    }

    public void ActivateXray()
    {
        foreach (GameObject XrayObject in XrayObjects)
        {
            XrayObject.SetActive(XrayState);
        }

        foreach (GameObject visibleObject in VisibleObjects)
        {
            visibleObject.SetActive(!XrayState);
        }
    }
}
