using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class CustomInteract : MonoBehaviour
{
    public UnityEvent interactEvent;

    private void OnMouseDown()
    {
        interactEvent.Invoke();

    }

}
