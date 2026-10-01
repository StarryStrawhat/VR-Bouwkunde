using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class XrayManager : MonoBehaviour
{
    public bool XrayState = false;
    public GameObject ToggleableObject;
    
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


        if (gameObject.tag == "Xray")
        {
            if (XrayState == true)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);

            }
        }

        if (gameObject.tag == "Visible")
        {
            if (XrayState == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
