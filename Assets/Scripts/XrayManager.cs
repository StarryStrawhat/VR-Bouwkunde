using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class XrayManager : MonoBehaviour
{
    [Header("XrayState")] //Boolean die regelt of Xray aan of uit staat.
    public bool XrayState = false;

    [Header("X-Ray 1.0")] //2 arrays waar je alle Xray & Visible tagged objecten in kan slepen in de Inspector.
    public GameObject[] XrayObjects;
    public GameObject[] VisibleObjects;

    [Header("X-Ray 2.0")] //Array met Objecten waarvan materiaal verwisselt.
    public GameObject[] TestCubes;
    //De 2 Materialen waar het object tussen wisselt.
    public Material XrayMaterial;
    public Material VisibleMaterial;
    

    //X-Ray 3.0 - Enum die gebruikt wordt om SurfaceType te veranderen.
    public enum SurfaceType
    {
        Opaque,
        Transparent
    }

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
        //X-Ray 1.0: 2 Objects zelfde plek, met druk van een knop Visible & X-Ray objects laten inspawnen/despawnen

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

        //X-Ray 2.0: 1 Object, wisselt het materiaal van het Object om zodat die doorzichtig wordt.

        //Wisselt het materiaal om
        SwapMaterial();

        //X-Ray 3.0 NOT WORKING: Verander de SurfaceType van het materiaal.

        //ChangeSurfaceType();

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

            //TestCube.GetComponent<MeshRenderer>().material = VisibleMaterial;
        }
    }


    //X-Ray 3.0 NOT WORKING
    //Checks XrayState, if true, verander Surface Type naar Opaque, anders Transparent.
    //THE ISSUE: Het werkt maar ik had per ongeluk het verkeerd om er in gedaan, dus als X-Ray aan stond werd materiaal Opaque.
    //           Ik dacht, oh oops, gewoon ff omwisselen. Maar for some fuckass reason werkt die dan niet goed.
    //           Hij wisselt namelijk wel de SurfaceType van het materiaal goed, maar het materiaal wordt niet actief veranderd in de scene.
    //           De manier hoe het nu staat, werkt het maar zit dus verkeerd om. Als je omdraait dan werkt het achter de schermen wel,
    //           maar niet visually. Hoe dit komt, geen idee.
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
        //Van het geselecteerde materiaal, zet de float SurfaceType naar Opaque
        VisibleMaterial.SetFloat("_Surface", (float)SurfaceType.Opaque);
    }
    public void TransparentSurfaceType()
    {
        //Van het geselecteerde materiaal, zet de float SurfaceType naar Transparent
        VisibleMaterial.SetFloat("_Surface", (float)SurfaceType.Transparent);
    }
}
