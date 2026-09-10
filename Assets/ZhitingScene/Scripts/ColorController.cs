using UnityEngine;
using UnityEngine.InputSystem;

public class ColorController : MonoBehaviour
{
    public InputActionReference colorChangeAction;
    private Light targetLight;
    public Color[] colors = { Color.white, Color.red, Color.green, Color.blue };
    private int currentColorIndex = 0;
    
    void OnEnable()
    {
        Debug.Log("Successful enable");
        colorChangeAction.action.Enable();
        targetLight = GetComponent<Light>();
        Debug.Log("获取到的Light组件是: " + targetLight);
        colorChangeAction.action.performed += ColorChange;
    }

    private void ColorChange(InputAction.CallbackContext obj)
    {
        Debug.Log("ColorChange active, current index is: " + currentColorIndex);
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        targetLight.color = colors[currentColorIndex];
        Debug.Log("Current color is: " + colors[currentColorIndex]);

    }

    void OnDisable()
    {
        colorChangeAction.action.Disable();
        colorChangeAction.action.performed -= ColorChange;
    }
}
