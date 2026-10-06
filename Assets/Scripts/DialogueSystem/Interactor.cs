using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    [SerializeField] private InputActionReference Interact;

    public Transform InteractorSource;
    public float InteractRange;

    void Start()
    {

    }

    void Update()
    {
        if (Interact.action.triggered) {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            } 
        }
    }
}
