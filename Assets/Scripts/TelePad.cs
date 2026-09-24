using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    [Header("Target Destination")]
    public Transform teleportTarget;

    [Header("Cooldown Settings")]
    [Tooltip("Time in seconds before this specific pad can be used again.")]
    public float cooldownTime = 1.5f;

    private float nextAllowedTeleportTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object is the Player
        if (!other.CompareTag("Player")) return;

        // 2. Check if the pad's individual cooldown has expired
        if (Time.time < nextAllowedTeleportTime) return;

        // Find the destination pad component if it exists
        TeleportPad targetPad = teleportTarget.GetComponent<TeleportPad>();

        if (targetPad != null)
        {
            // 3. Put the receiving pad on cooldown immediately *before* moving the player
            targetPad.TriggerCooldown();
        }

        // 4. Move the player
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        other.transform.position = teleportTarget.position;
        other.transform.rotation = teleportTarget.rotation;

        if (cc != null) cc.enabled = true;
        Physics.SyncTransforms();

        // 5. Put this pad on cooldown too, just to be safe
        TriggerCooldown();
    }

    // Public method so the sending pad can trigger a cooldown on the receiving pad
    public void TriggerCooldown()
    {
        nextAllowedTeleportTime = Time.time + cooldownTime;
    }
}

