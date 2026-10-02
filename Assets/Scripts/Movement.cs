using UnityEngine;
using UnityEngine.InputSystem;

public class Movement: MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    public float moveSpeed;

    void Start()
    {
        //zorgt ervoor dat dit script de inputsystem vind
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }
    void Update()
    {
       MovePlayer(); 
    }

    public void MovePlayer()
    {
            //zorgt ervoor dat de speler horizontaal kan bewegen
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y)* moveSpeed * Time.deltaTime;    
    }
}


