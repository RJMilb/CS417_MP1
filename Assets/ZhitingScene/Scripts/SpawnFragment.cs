using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpawnFragment : MonoBehaviour
{
    public InputActionReference spawnAction;
    public GameObject fragmentPrefab;

    private XRGrabInteractable grabInteractable;

    public Transform muzzlePoint;



    void OnEnable()
    {
        spawnAction.action.Enable();
        spawnAction.action.performed += SpawnObject;
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void SpawnObject(InputAction.CallbackContext obj)
    {
        if (grabInteractable.isSelected)
        {
            Instantiate(fragmentPrefab, muzzlePoint.position, muzzlePoint.rotation);
        }
    }

    void OnDisable()
    {
        spawnAction.action.Disable();
        spawnAction.action.performed -= SpawnObject;
    }
}
