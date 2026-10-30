using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{

    // bepaalt de looksensitivity
    [SerializeField] private float LookSensitivity = 0.2f;

    // Hiermee wordt de maximale kijkhoek ingesteld zodat je niet door je hoofd heen kijkt.
    [SerializeField] private float LookAngleLimit = 90f;

    // Dit houdt de camera-component bij die als child van dit object zit.
    private Camera mainCamera;

    // Deze waarde bewaart de huidige verticale kijkhoek.
    private float Lookangle = 0f;

    private void Start()
    {
        // Zoek de Camera die als child van dit object staat.
        mainCamera = GetComponentInChildren<Camera>();

        // // Vergrendel de cursor en maak hem onzichtbaar
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        // Lees de muisdelta uit het Input System en geef deze door aan de HandleLooking-functie.
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        HandleLooking(mouseDelta);
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        // Verhoog of verlaag de verticale kijkhoek op basis van de muisbeweging.
        Lookangle += -mouseDelta.y * LookSensitivity;

        // Beperk de hoek zodat je niet boven of onder de wereld uit kijkt.
        Lookangle = Mathf.Clamp(Lookangle, -LookAngleLimit, LookAngleLimit);

        // Draai de camera verticaal om de kijkhoek te laten aanpassen.
        mainCamera.transform.localRotation = Quaternion.Euler(Lookangle, 0, 0);

        // Draai het object horizontaal zodat je links en rechts kunt kijken.
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * LookSensitivity, 0);
    }
}
