using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Container : MonoBehaviour
{
    public GameObject cellContainer;
 public XRGrabInteractable cellContainerGrabScript;
    private bool isAssembled = false;

    public void OnCombine()
    {
        cellContainerGrabScript.enabled = false;
        cellContainer.GetComponent<Collider>().enabled = false;
        cellContainer.transform.SetParent(transform);
        cellContainer.transform.localPosition = Vector3.zero;
        isAssembled = true;
    }

    void LateUpdate()
    {
        if (isAssembled)
        {
            Rigidbody rb = cellContainer.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
        }
    }
}