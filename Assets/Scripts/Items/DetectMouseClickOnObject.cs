using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMouseClickOnObject : MonoBehaviour
{
    public Camera myCamera; //The camera from which the ray is cast.
    public ItemManager itemManager; //ItemManager script, used to reference booleans/functions from there.
    public XrayManager xrayManager; //XrayManager script, used to reference notifications.
    private float length = 2f; //To extend length of OnDrawGizmo DrawRay
    Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0); //The point from which the ray is cast inside the camera.

    public void ClickItem(InputAction.CallbackContext context)
    {
       if (!context.started) //Als dit niet de eerste keer is dat je signaal binnenkrijgt dan stopt ie meteen.
            return;         //Met 1 key input geef je vaak meer dan 1 input signal.
                            //Om ervoor te zorgen dat je maar 1x de xray toggled stopt die als het niet de eerste input is.

        //This ray is using the center point of the camera to be cast, originally this was supposed to use mouse position but that didn't work.
        //When using mouse position the ray would be stock on the bottom left corner of the camera, this was tested using OnDrawGizmos.
        Ray searchingItem = myCamera.ScreenPointToRay(center);
        
        //Returns information of object hit by ray.
        RaycastHit raycastHit;

        //Define boolean to check if you hit something.
        //Boolean returns true if ray has hit something.
        bool weHitSomething = Physics.Raycast(searchingItem, out raycastHit);

        //If you hit something with the ray, get it's name.
        if (weHitSomething)
        {
            Debug.Log(raycastHit.transform.name);
            //Check if the object you hit has the Item tag
            if (raycastHit.transform.CompareTag("Item"))
            {
                Debug.Log("ITEM FOUND ITEM FOUND");
                //If hit object has Item tag, check for name
                if (raycastHit.transform.name == "DoorKeyTest")
                {
                    itemManager.DoorKeyFound = true; //DoorKeyFound is now true, which allows GrabDoorKey() to activate
                    itemManager.FlipXrayAllow(); //XrayAllow is flipped off
                    itemManager.GrabDoorKey(); //DoorKey object is now inactive
                    xrayManager.ShowNotification(); //Show message "Battery empty" & Delete messages "X-Ray on/off"
                }

                if (raycastHit.transform.name == "BatteryTest" && itemManager.DoorKeyFound == true)
                {
                    //If Battery was hit & Door Key has been found
                    itemManager.BatteryFound = true; //BatteryFound is now true, which allows GrabBattery() to activate
                    itemManager.FlipXrayAllow(); //XrayAllow is flipped back on 
                    itemManager.GrabBattery(); //Battery object is now inactive
                    xrayManager.notifMessages[2].SetActive(false); //Delete message "Battery Empty..."
                    xrayManager.notifMessages[3].SetActive(true); //Show message "Battery Recharged!"

                }

                // Template for future items:
                //if (raycastHit.transform.name == "[ITEMNAME]")
                //{
                //    itemManager.[ITEMNAME]Found = true //[ITEMNAME]Found is now true, which allows Grab[ITEMNAME]() to activate
                //    itemManager.Grab[ITEMNAME](); //[ITEMNAME] is now inactive
                //}
            }
            else
            {
                Debug.Log("Looking for item...."); //Return this if you don't hit an item.
            }

        }
        else
        {
            //Don't hit anything? Do nothing.
            Debug.Log("We don't hit anything");
        }
    }

    public void OnDrawGizmos() //This function is purely for visualizing the Ray that's being cast.
    {
        //Cast a ray through the center point of the screen. This Vector is already defined (Vector3 center)
        //Visualize the ray with a gizmo.
        Ray testRay = myCamera.ScreenPointToRay(center); //Shoot a test ray at same point as searchingItem
        Gizmos.color = Color.yellow; //Set color of Gizmo
        Gizmos.DrawRay(testRay.origin, testRay.direction * length); //Draw a ray on the same points as testRay
    }

}
