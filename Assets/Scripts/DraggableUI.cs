using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableUI : MonoBehaviour
{
    bool _dragging = false;

    private void Update()
    {
        print(Mouse.current.leftButton.wasPressedThisFrame);

    }

}
