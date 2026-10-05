using UnityEngine;
using UnityEngine.InputSystem;

public class Movement: MonoBehaviour
{
    //Called de Input System en de Move Action uit de Input System en zorgt ervoor dat je ze kan referencen in script.
    PlayerInput playerInput;
    InputAction moveAction;
    //Float die de movement speed bepaald.
    public float moveSpeed;

    void Start()
    {
        //Zorgt ervoor dat dit script de InputSystem vindt.
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }
    void Update()
    {
       MovePlayer(); 
    }

    public void MovePlayer()
    {
            //Zorgt ervoor dat de speler vrij kan bewegen op 2 axes.
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y)* moveSpeed * Time.deltaTime;    
    }
}


