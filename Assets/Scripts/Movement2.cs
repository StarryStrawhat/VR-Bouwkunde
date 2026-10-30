using UnityEngine;
using UnityEngine.InputSystem;

public class Movement2 : MonoBehaviour
{
    //Called de Input System en de Move Action uit de Input System en zorgt ervoor dat je ze kan referencen in script.
    PlayerInput playerInput;
    InputAction moveAction;
    //Float die de movement speed bepaald.
    public float moveSpeed = 5f;
    //Camera die de kijkrichting van de speler bepaald.
    public Camera playerCamera;
    private CharacterController controller;

    private void Start()
    {
        //Zorgt ervoor dat dit script de InputSystem vindt.
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // Roep de bewegingsfunctie elke frame aan zodat de speler constant kan bewegen.
        MovePlayer();


    }

    public void MovePlayer()
    {
        //Zorgt ervoor dat de speler vrij kan bewegen op 2 axes.
        Vector2 direction = moveAction.ReadValue<Vector2>();

        if (playerCamera == null)
        {
            // Als er nog geen camera gekoppeld is, gebruik dan de hoofdcamera van de scene.
            playerCamera = Camera.main;
            if (playerCamera == null) return;
        }

        // Haal de voor- en rechtervector van de camera op.
        // Deze worden gebruikt om de beweging te relateren aan de kijkrichting.
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Zet de Y-component weg, zodat de speler niet verticaal beweegt.
        forward.y = 0f;
        right.y = 0f;

        //snelheid blijft consistent
        forward.Normalize();
        right.Normalize();

        //berekent uiteindelijke beweging
        Vector3 move = (forward * direction.y + right * direction.x) * moveSpeed;

        controller.Move(move * Time.deltaTime);
    }
}


