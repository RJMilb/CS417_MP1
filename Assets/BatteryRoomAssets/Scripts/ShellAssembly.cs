using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShellAssembly : MonoBehaviour
{
    public GameObject capPiece;
    public XRGrabInteractable capGrabScript;
    private bool isAssembled = false;

    public void OnAssemblyComplete()
    {
        capGrabScript.enabled = false;
        capPiece.GetComponent<Collider>().enabled = false;
        capPiece.transform.SetParent(transform);
        isAssembled = true;
    }

    void LateUpdate()
    {
        if (isAssembled)
        {
            Rigidbody rb = capPiece.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            capPiece.transform.localPosition = Vector3.zero;
            capPiece.transform.localRotation = Quaternion.identity;
        }
    }
}