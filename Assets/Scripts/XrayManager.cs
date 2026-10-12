using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class XrayManager : MonoBehaviour
{
    [Header("State and Objects")] 
    //Boolean die regelt of Xray aan of uit staat.
    //Daaronder staan 2 arrays waar je alle Xray & Visible tagged objecten in kan slepen in de Inspector.
    public bool XrayState = false;
    public GameObject[] XrayObjects;
    public GameObject[] VisibleObjects;

    [Header("Materials")]
    public Material XrayMaterial;
    public Material VisibleMaterial;

    public enum SurfaceType
    {
        Opaque,
        Transparent
    }

    public GameObject[] TestCubes;

    void Start()
    {
        //Deze staat hier om de Xray direct te activeren en niet pas wanneer je op spacebar klikt.
        ActivateXray();
        //Deze staat hier om ervoor te zorgen dat VisibleMaterial Opaque is bij opstarten.
        //Als ChangeSurfaceType verwijderd wordt zal dit ook weggaan.
        OpaqueSurfaceType();
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

        SwapMaterial();

        //ChangeSurfaceType();

    }

    //Checks XrayState, if true, zet het materiaal van geselecteerde Object naar
    // materiaal A, if false, zet materiaal naar materiaal B.
    public void SwapMaterial()
    {
        if (XrayState == true)
        {
            foreach (GameObject TestCube in TestCubes)
            {
                TestCube.GetComponent<MeshRenderer>().material = XrayMaterial;
            }

            //Kijkt naar MeshRenderer component, veranderd het materiaal.
            //TestCube.GetComponent<MeshRenderer>().material = XrayMaterial;
        }
        else
        {
            foreach (GameObject TestCube in TestCubes)
            {
                TestCube.GetComponent<MeshRenderer>().material = VisibleMaterial;
            }

            //TestCube.GetComponent<MeshRenderer>().material = VisibleMaterial;
        }
    }




    //Changing Surface Type is currently not working.
    //The idea was to change the Surface Type of the selected material,
    //when pressing the x-ray input.
    public void ChangeSurfaceType()
    {
        if (XrayState == true)
        {
            OpaqueSurfaceType();
            Debug.Log("Material is now opaque");
        }
        else
        {
            TransparentSurfaceType();
            Debug.Log("Material is now transparent");
        }
    }
    public void OpaqueSurfaceType()
    {
        VisibleMaterial.SetFloat("_Surface", (float)SurfaceType.Opaque);
    }
    public void TransparentSurfaceType()
    {
        VisibleMaterial.SetFloat("_Surface", (float)SurfaceType.Transparent);
    }
}
