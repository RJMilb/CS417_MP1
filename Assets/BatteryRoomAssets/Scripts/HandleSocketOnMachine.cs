using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandleSocketOnMachine : MonoBehaviour
{
   
    public EscapeManager escapeManager;
    public XRGrabInteractable handle;
    public Vector3 insertOffset;

    public void OnHandleInserted()
    {

        handle.enabled = false;
        Rigidbody rb = handle.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        handle.transform.position = transform.position + insertOffset;
        handle.transform.rotation = transform.rotation;

        if (escapeManager != null) escapeManager.MarkHandleComplete();
    }

}
