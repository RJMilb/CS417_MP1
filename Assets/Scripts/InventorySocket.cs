using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PersistentSocketHandler : MonoBehaviour
{
    //[Header("Scaling Settings")]
    //[Tooltip("Scale multiplier when inside the socket (e.g., 0.5 = half size)")]
    //public float socketScaleMultiplier = 0.5f;

    private XRSocketInteractor socketInteractor;
    private bool isDeactivating = false;

    // Track original scales using the Interactable transform as the key
    private Dictionary<Transform, Vector3> originalScales = new Dictionary<Transform, Vector3>();

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        isDeactivating = false;
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        isDeactivating = true;
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject is XRBaseInteractable interactable)
        {
            Transform target = interactable.transform;

            // 1. Save original scale before any modification
            //if (!originalScales.ContainsKey(target))
            //{
            //    originalScales[target] = target.localScale;
            //}

            // 2. Parent it to the socket attach transform
            target.SetParent(socketInteractor.attachTransform, true);

            // 3. Set the tiny scale
            //target.localScale = originalScales[target] * socketScaleMultiplier;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject is XRBaseInteractable interactable)
        {
            Transform target = interactable.transform;

            // CRITICAL FIX: If the drop happened because the inventory bag closed,
            // DO NOT touch anything. Let it stay tiny and stay parented!
            if (isDeactivating || !gameObject.activeInHierarchy) return;

            // If the player grabbed it out naturally:
            // 1. Instantly unparent it (No Coroutine)
            target.SetParent(null, true);

            // 2. Instantly restore its full size
            //if (originalScales.ContainsKey(target))
            //{
            //    target.localScale = originalScales[target];
            //    originalScales.Remove(target); // Clear from memory
            //}
        }
    }
}