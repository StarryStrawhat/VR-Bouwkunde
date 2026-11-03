using UnityEngine;
using UnityEngine.InputSystem;

public class ClueToggle : MonoBehaviour
{
    public InputActionReference ClueLogToggle;
    bool ToggleClue = false; 

    void Update()
    {
        if (ClueLogToggle.action.WasCompletedThisFrame() && !ToggleClue)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ToggleClue = true;
        }
        else if (ClueLogToggle.action.WasCompletedThisFrame() && ToggleClue)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ToggleClue = false;
        }
    }

}
