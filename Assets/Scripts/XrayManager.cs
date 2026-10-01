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
        
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    Debug.Log("Space bar pressed");
        //    XrayState = !XrayState;
        //    Debug.Log("XrayState flipped!!!");
        //}

        if (XrayState == true)
        {
            XrayObjects[0].SetActive(true);
            VisibleObjects[0].SetActive(false);
        }
        else
        {
            XrayObjects[0].SetActive(false);
            VisibleObjects[0].SetActive(true);
        }

    }
}
