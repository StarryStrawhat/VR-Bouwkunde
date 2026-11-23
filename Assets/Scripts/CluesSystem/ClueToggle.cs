using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClueToggle : MonoBehaviour
{
    //Called de ClueLogToggle action uit de input system en zorgt dat je kunt referencen in je script 
    public InputActionReference ClueLogToggle;
    //Called de bookSystem en zorgt dat je kunt referencen in je script 
    public GameObject bookSystem;
    //reference naar de movement en bruikbaar maken in eigen script
    public Movement2 movement;
    //Refrence naar de camera en bruikbaar maken in eigen script
    public FirstPersonCamera ClueCamera;
    //Een Boolean die regelt of de toggle aan of uit staat
    bool ToggleClue = false;
    //boolean of de clue book zichtbaar is of niet
    bool ClueLogVisible = false;

    //verstopt de clue book als het spel start
    private void Start()
    {
        HideBookSystem();
    }

    //vertoont de Clue boek
    private void ShowBookSystem()
    {
      bookSystem.SetActive(true);
      ClueLogVisible = true;
    }

    //verstopt de clue book
    private void HideBookSystem()
    {
        bookSystem.SetActive(false);
        ClueLogVisible = false;
    }

    void Update()
    {
        //checkt of de cluetoggle input knop wordt ingedrukt en of je de clue boekje nog niet open hebt staan.
        if (ClueLogToggle.action.WasPressedThisFrame() && !ToggleClue && !ClueLogVisible)
        {
            Debug.Log("Toggle triggered");
            //onvergrendeld de muis
            Cursor.lockState = CursorLockMode.None;
            //maakt de muis visible
            Cursor.visible = true;
            //Activeert de ClueToggle switch
            ToggleClue = true;
            ShowBookSystem();
            //Haalt script op van Movement2 script en zorgt dat de movement freezed
            movement.Freeze();
            //haalt script op van FirstPersonCamera script en zorgt dat de camera freezed
            ClueCamera.FreezeCam();
        }
        //checkt of de cluetoggle input wordt ingedrukt en of die actief is
        else if (ClueLogToggle.action.WasPressedThisFrame() && ToggleClue && ClueLogVisible)
        {
            Debug.Log("Toggle triggered");
            //vergrendeld de muis
            Cursor.lockState = CursorLockMode.Locked;
            //maakt de muis invisible
            Cursor.visible = false;
            //Deactiveert de switch
            ToggleClue = false;
            //verstopt de clueLog
            HideBookSystem();
            //Haalt script op van Movement2 script en zorgt dat de movement unfreezed
            movement.Unfreeze();
            //Haalt script op van FirstPersonCamera script en zorgt dat de camera unfreezed
            ClueCamera.UnFreezeCam();
        }
    }
}
