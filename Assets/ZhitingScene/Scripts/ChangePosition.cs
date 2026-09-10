using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ChangePosition : MonoBehaviour
{
    public InputActionReference teleportAction;
    public Vector3 insidePosition;
    public Vector3 outsidePosition;
    public Transform xrOrigin;
    private bool isOutside = false;
    
    
    void OnEnable()
    {
        teleportAction.action.Enable();
        teleportAction.action.performed += ChangeSide;
    }

    private void ChangeSide(InputAction.CallbackContext obj)
    {
        if (!isOutside)
        {
            xrOrigin.transform.position = outsidePosition;
        }
        else
        {
            xrOrigin.transform.position = insidePosition;
        }
        isOutside = !isOutside;
    }

    // Update is called once per frame
    void OnDisable()
    {
        teleportAction.action.Disable();
        teleportAction.action.performed -= ChangeSide;
    }
}
