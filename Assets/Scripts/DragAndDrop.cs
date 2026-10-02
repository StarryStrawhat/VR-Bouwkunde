using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    float _dis;
    GameObject dragTestObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            print("MouseDown");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
            {
                if (hit.collider.gameObject.layer != LayerMask.GetMask("Ignore Raycast"))
                {
                    _dis = Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position);
                    dragTestObj = hit.collider.gameObject;
                }
            }

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            print("MouseUp");
            dragTestObj = null;
        }

        if (dragTestObj != null)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.farClipPlane * .1f;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 point = Vector3.MoveTowards(Camera.main.transform.position, worldPoint, _dis);

            dragTestObj.transform.position = point;
        }
    }

    public void ClickTest()
    {
        print("Hello World!");
    }

}
