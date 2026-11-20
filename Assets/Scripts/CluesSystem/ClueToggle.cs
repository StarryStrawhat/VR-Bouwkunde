using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClueToggle : MonoBehaviour
{
    //Called de ClueLogToggle action uit de input system en zorgt dat je kunt referencen in je script 
    public InputActionReference ClueLogToggle;
    public GameObject bookSystem;
    //Een Boolean die regelt of de toggle aan of uit staat
    bool ToggleClue = false;
    bool Visible = false;

    private void Start()
    {
        HideBookSystem();
    }

    private void ShowBookSystem()
    {
      bookSystem.SetActive(true);
      Visible = true;
    }

    private void HideBookSystem()
    {
        bookSystem.SetActive(false);
        Visible = false;
    }

    void Update()
    {
        //checkt of de cluetoggle input knop wordt ingedrukt en of de niet cluetoggle actief is
        if (ClueLogToggle.action.WasPressedThisFrame() && !ToggleClue && !Visible)
        {
            Debug.Log("Toggle triggered");
            //onvergrendeld de muis
            Cursor.lockState = CursorLockMode.None;
            //maakt de muis visible
            Cursor.visible = true;
            //Activeert de ClueToggle switch
            ToggleClue = true;
            ShowBookSystem();
        }
        //checkt of de cluetoggle input wordt ingedrukt en of die actief is
        else if (ClueLogToggle.action.WasPressedThisFrame() && ToggleClue && Visible)
        {
            Debug.Log("Toggle triggered");
            //vergrendeld de muis
            Cursor.lockState = CursorLockMode.Locked;
            //maakt de muis invisible
            Cursor.visible = false;
            //Deactiveert de switch
            ToggleClue = false;
            HideBookSystem();
        }
    }
}
