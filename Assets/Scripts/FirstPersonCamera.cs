using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{

    // public Transform player;
    // public float mouseSensitivity = 2f;
    // float cameraverticalRotation = 0f;
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90f;

    private Camera mainCamera;
    private float Lookangle = 0f;

    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        // float inputX=Input.GetAxis("Mouse X") * mouseSensitivity;
        // float inputY=Input.GetAxis("Mouse Y") * mouseSensitivity;

        // cameraverticalRotation -= inputY;
        // cameraverticalRotation = Mathf.Clamp(cameraverticalRotation, -90f, 90f);
        // transform.localEulerAngles=Vector3.right * cameraverticalRotation;
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        HandleLooking(mouseDelta);
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        Lookangle += -mouseDelta.y * LookSensitivity;
        Lookangle = Mathf.Clamp(Lookangle, -LookAngleLimit, LookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(Lookangle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * LookSensitivity, 0);
    }
}
