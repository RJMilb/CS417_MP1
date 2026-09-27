using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BatterySocket : MonoBehaviour
{
    public WinSystem winSystem;
    public XRGrabInteractable battery;
    public Vector3 insertOffsets;
    public Vector3 rotationOffsets;

    public void OnBatteryInserted()
    {
        battery.enabled = false;
        Rigidbody rb = battery.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        battery.transform.position = transform.position + insertOffsets;
        battery.transform.rotation = transform.rotation * Quaternion.Euler(rotationOffsets);

        if (winSystem != null) winSystem.MarkBatteryInserted();
    }

}
