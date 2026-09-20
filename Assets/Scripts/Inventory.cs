using UnityEngine;
using UnityEngine.InputSystem;
public class Inventory : MonoBehaviour
{
    public InputActionReference action;
    public GameObject wristUI;
    private bool open = true;

    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            open = !open;
            wristUI.SetActive(open);
        };
    }
}
