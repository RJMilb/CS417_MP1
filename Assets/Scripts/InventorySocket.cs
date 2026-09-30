using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PersistentSocketHandler : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private bool isDeactivating = false;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Make the whole rig persistent (do this only once, on the root).
        // Ideally put this on the rig's root script instead, and guard
        // against duplicates if the rig scene can be loaded again.
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void OnEnable()
    {
        isDeactivating = false;
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        isDeactivating = true;
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        socketInteractor.selectExited.RemoveListener(OnSelectExited);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Make sure the socket still has a valid manager.
        // If you keep one manager on the persistent rig, this is just a safety net.
        if (socketInteractor.interactionManager == null)
        {
            var manager = FindFirstObjectByType<XRInteractionManager>();
            if (manager != null)
                socketInteractor.interactionManager = manager;
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject is XRBaseInteractable interactable)
        {
            // Parenting to a DontDestroyOnLoad object moves it into that scene too.
            interactable.transform.SetParent(socketInteractor.attachTransform, true);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (isDeactivating || !gameObject.activeInHierarchy) return;

        if (args.interactableObject is XRBaseInteractable interactable)
        {
            Transform target = interactable.transform;
            target.SetParent(null, true);

            // Move it out of the DontDestroyOnLoad scene, or it will
            // follow you into every future scene.
            SceneManager.MoveGameObjectToScene(target.gameObject, SceneManager.GetActiveScene());
        }
    }
}