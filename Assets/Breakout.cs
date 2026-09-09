using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference action;
    public Transform outsideViewPoint;

    private Vector3 roomPosition;
    private Quaternion roomRotation;

    private bool outside = false;

    void Start()
    {
        roomPosition = transform.position;
        roomRotation = transform.rotation;

        action.action.Enable();

        action.action.performed += (ctx) =>
        {
            if (!outside)
            {
                transform.position = outsideViewPoint.position;
                transform.rotation = outsideViewPoint.rotation;
                outside = true;
            }
            else
            {
                transform.position = roomPosition;
                transform.rotation = roomRotation;
                outside = false;
            }
        };
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame) {
            if (!outside)
            {
                transform.position = outsideViewPoint.position;
                transform.rotation = outsideViewPoint.rotation;
                outside = true;
            }
            else
            {
                transform.position = roomPosition;
                transform.rotation = roomRotation;
                outside = false;
            }
        }
    }
}