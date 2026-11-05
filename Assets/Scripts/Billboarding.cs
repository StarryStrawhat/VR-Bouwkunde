using UnityEngine;

// Deze script kan voor iedereen gebruikt worden. Deze script zorgt voor dat een canvas altijd in richting van een speler kijkt
public class Billboarding : MonoBehaviour
{
    //referenced naar de camera die wij willen gebruiken
    [SerializeField] private Camera mainCamera;

    private void Reset()
    {
        mainCamera = Camera.main;
    }

    //Berekent waar de camera naar kijkt. Hierdoor kijkt de canvas altijd naar de camera
    private void Update()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
