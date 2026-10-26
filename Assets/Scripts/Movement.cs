using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Called de Input System en de Move Action uit de Input System en zorgt ervoor dat je ze kan refereren in script.
    PlayerInput playerInput;
    InputAction moveAction;

    // Float die de movement speed bepaald.
    public float moveSpeed = 5f;

    // Camera die de kijkrichting van de speler bepaald.
    public Camera playerCamera;

    private Rigidbody rb;
    private Vector2 inputDirection;

    private void Start()
    {
        // Zorgt ervoor dat dit script de InputSystem vindt.
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

        // Haal de Rigidbody component op
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Lees de input elke frame in Update
        if (moveAction != null)
        {
            inputDirection = moveAction.ReadValue<Vector2>();
        }

        // Als er nog geen camera gekoppeld is, gebruik dan de hoofdcamera van de scene.
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    // FixedUpdate wordt gebruikt voor alle physics berekeningen (loopt synchroon met de physics engine)
    private void FixedUpdate()
    {
        if (playerCamera == null) return;

        // Haal de voor- en rechtervector van de camera op
        // Deze worden gebruikt om de beweging te relateren aan de kijkrichting.
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Zet de Y-component weg, zodat de speler niet verticaal beweegt.
        forward.y = 0f;
        right.y = 0f;

        // Snelheid blijft consistent
        forward.Normalize();
        right.Normalize();

        // Bereken de gewenste richting op basis van de joystick input
        Vector3 moveDirection = (forward * inputDirection.y + right * inputDirection.x);

        // Bereken de doelsnelheid (X en Z) en behoud de huidige Y-snelheid zodat zwaartekracht/vallen blijft werken
        Vector3 targetVelocity = moveDirection * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y;

        // Pas de snelheid direct toe op de Rigidbody in plaats van de transform positie aan te passen
        rb.linearVelocity = targetVelocity;
    }
}




