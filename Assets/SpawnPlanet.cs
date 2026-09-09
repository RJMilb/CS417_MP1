using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlanet : MonoBehaviour
{
    public InputActionReference action;
    public GameObject planetPrefab;

    void Start()
    {
        action.action.Enable();

        action.action.performed += (ctx) =>
        {
            Instantiate(planetPrefab, new Vector3(0,7,0), transform.rotation);
        };
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame) {
            Instantiate(planetPrefab, new Vector3(0,7,0), transform.rotation);
        }
    }
}
