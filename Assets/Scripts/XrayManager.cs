using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class XrayManager : MonoBehaviour
{
    //Boolean die regelt of Xray aan of uit staat.
    //Daaronder staan 2 arrays waar je alle Xray & Visible tagged objecten in kan slepen in de Inspector.
    public bool XrayState = false;
    public GameObject[] XrayObjects;
    public GameObject[] VisibleObjects;
    void Start()
    {
        //Deze staat hier om de Xray direct te activeren en niet pas wanneer je op spacebar klikt.
        ActivateXray();
    }
    
    //Dit is de functie die de XrayState boolean flipped wanneer input wordt waargenomen.
    public void OnXrayToggle(InputAction.CallbackContext context)
    {
        //Als dit niet de eerste keer is dat je signaal binnenkrijgt dan stopt ie meteen.
        //Met 1 key input geef je vaak meer dan 1 input signal. 
        //Om ervoor te zorgen dat je maar 1x de xray toggled stopt die als het niet de eerste input is.
        if (!context.started)
            return;
       //Dit flipped de boolean
        XrayState = !XrayState;
        //Toggled de xray aan/uit
        ActivateXray();
    }

    public void ActivateXray()
    {
        //Zet voor elk item in de array XrayObjects de SetActive gelijk aan XrayState.
        foreach (GameObject XrayObject in XrayObjects)
        {
            XrayObject.SetActive(XrayState);
        }

        //Zet voor elk item in de array VisibleObjects de SetActive gelijk aan de opposite van XrayState.
        foreach (GameObject visibleObject in VisibleObjects)
        {
            visibleObject.SetActive(!XrayState);
        }
    }
}
