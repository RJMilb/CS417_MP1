using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public InputActionReference action;

    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();

        action.action.Enable();

        action.action.performed += (ctx) =>
        {
            lightComponent.color = Color.red;
        };
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame) {
            lightComponent.color = Color.red;
        }
    }
}
