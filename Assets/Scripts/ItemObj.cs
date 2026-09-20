using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketRescaler : MonoBehaviour
{
    public Vector3 socketedScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 originalScale;
    private XRGrabInteractable grabInteractable;

    void OnEnable()
    {
        originalScale = transform.localScale;
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Automatically subscribe to socket/grab events
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check if the thing selecting us is an XR Socket Interactor
        if (args.interactorObject is XRSocketInteractor)
        {
            transform.localScale = socketedScale;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor)
        {
            transform.localScale = originalScale;
        }
    }
}
