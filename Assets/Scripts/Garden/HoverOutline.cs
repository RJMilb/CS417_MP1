using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; 

[RequireComponent(typeof(XRGrabInteractable))]
public class HoverOutline : MonoBehaviour
{
    public Material outlineMaterial;
    private GameObject outlineObject;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        // Find the primary mesh on the object or its children
        MeshFilter originalMesh = GetComponentInChildren<MeshFilter>();
        if (originalMesh != null)
        {
            // Generate the outline mesh object
            outlineObject = new GameObject("OutlineMesh");
            outlineObject.transform.SetParent(originalMesh.transform, false);
            outlineObject.transform.localPosition = Vector3.zero;
            outlineObject.transform.localRotation = Quaternion.identity;
            
            // Scale up by 5% to create the border thickness
            outlineObject.transform.localScale = Vector3.one * 1.05f; 

            outlineObject.AddComponent<MeshFilter>().sharedMesh = originalMesh.sharedMesh;
            MeshRenderer outlineRenderer = outlineObject.AddComponent<MeshRenderer>();
            outlineRenderer.material = outlineMaterial;
            
            outlineObject.SetActive(false);
        }

        // Automatically hook into the controller hover events
        grabInteractable.hoverEntered.AddListener(OnHoverEnter);
        grabInteractable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (outlineObject != null) outlineObject.SetActive(true);
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (outlineObject != null) outlineObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.RemoveListener(OnHoverEnter);
            grabInteractable.hoverExited.RemoveListener(OnHoverExit);
        }
    }
}