using UnityEngine;
using UnityEngine.InputSystem;

public class ClueToggle : MonoBehaviour
{
    //Called de ClueLogToggle action uit de input system en zorgt dat je kunt referencen in je script 
    public InputActionReference ClueLogToggle;
    //Een Boolean die regelt of de toggle aan of uit staat
    bool ToggleClue = false; 

    void Update()
    {
        //checkt of de cluetoggle input knop wordt ingedrukt en of de niet cluetoggle actief is
        if (ClueLogToggle.action.WasCompletedThisFrame() && !ToggleClue)
        {
            //onvergrendeld de muis
            Cursor.lockState = CursorLockMode.None;
            //maakt de muis visible
            Cursor.visible = true;
            //Activeert de ClueToggle switch
            ToggleClue = true;
        }
        //checkt of de cluetoggle input wordt ingedrukt en of die actief is
        else if (ClueLogToggle.action.WasCompletedThisFrame() && ToggleClue)
        {
            //vergrendeld de muis
            Cursor.lockState = CursorLockMode.Locked;
            //maakt de muis invisible
            Cursor.visible = false;
            //Deactiveert de switch
            ToggleClue = false;
        }
    }

}
