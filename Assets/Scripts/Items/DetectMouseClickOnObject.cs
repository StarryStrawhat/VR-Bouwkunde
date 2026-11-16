using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMouseClickOnObject : MonoBehaviour
{
    public Camera myCamera;
    public ItemManager itemManager;
    private float length = 2f;
    Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0);

    public void ClickItem(InputAction.CallbackContext context)
    {
       if (!context.started)
            return;

        //This ray is using the center point of the camera to be cast, originally this was supposed to use mouse position but that didn't work.
        //When using mouse position the ray would be stock on the bottom left corner of the camera, this was tested using OnDrawGizmos.
        Ray myRay = myCamera.ScreenPointToRay(center);
        
        //Returns information of object hit by ray.
        RaycastHit raycastHit;

        //Define boolean to check if you hit something.
        //Boolean returns true if ray has hit something.
        bool weHitSomething = Physics.Raycast(myRay, out raycastHit);

        //If you hit something with the ray, get it's name.
        if (weHitSomething)
        {
            Debug.Log(raycastHit.transform.name);
            //Check if the object you hit has the Item tag
            if (raycastHit.transform.CompareTag("Item"))
            {
                Debug.Log("ITEM FOUND ITEM FOUND");

                if (raycastHit.transform.name == "BatteryTest")
                {
                    itemManager.BatteryFound = true;
                    itemManager.FlipXrayAllow();
                    itemManager.GrabBattery();
                }

                if (raycastHit.transform.name == "DoorKeyTest")
                {
                    itemManager.DoorKeyFound = true;
                    itemManager.FlipXrayAllow();
                    itemManager.GrabDoorKey();
                }

                // Template for future items:
                //if (raycastHit.transform.name == "ITEM NAME")
                //{
                //    itemManager.(ITEM NAME)Found = true
                //    itemManager.Grab(ITEM NAME)();
                //}
            }
            else
            {
                Debug.Log("Looking for item....");
            }

        }
        else
        {
            //Don't hit anything? Do nothing.
            Debug.Log("We don't hit anything");
        }
    }

    public void OnDrawGizmos()
    {
        //Cast a ray through the center point of the screen. This Vector is already defined ^^
        //Visualize the ray with a gizmo.
        Ray testRay = myCamera.ScreenPointToRay(center);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(testRay.origin, testRay.direction * length);
    }

}
