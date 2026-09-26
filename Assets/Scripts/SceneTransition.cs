using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class SceneTransition : MonoBehaviour
{
    public enum RoomDestination
    {
        MaintenanceRoom,
        GardenRoom,
        ComputerRoom,
        BatteryRoom
    }

    [Header("Destination")]
    [SerializeField] private RoomDestination targetRoom;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable _interactable;
    private bool _isLoading = false;

    private void Awake()
    {
        _interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        // selectEntered fires when the player pulls the grip/trigger while aiming or touching
        _interactable.selectEntered.AddListener(OnDoorActivated);
    }

    private void OnDisable()
    {
        _interactable.selectEntered.RemoveListener(OnDoorActivated);
    }

    private void OnDoorActivated(SelectEnterEventArgs args)
    {
        if (_isLoading) return;
        _isLoading = true;

        string targetSceneName = targetRoom.ToString();
        SceneManager.LoadSceneAsync(targetSceneName);
    }
}