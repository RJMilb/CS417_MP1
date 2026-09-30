using System.Collections;
using UnityEngine;

public class SafeController : MonoBehaviour
{
    [Header("Lid Settings")]
    [Tooltip("Drag the Lid's Transform here.")]
    public Transform lidTransform;
    
    [Tooltip("The starting rotation of the lid (in Local Euler Angles).")]
    public Vector3 closedRotation = new Vector3(0f, 0f, -180f);
    
    [Tooltip("The final target rotation of the lid.")]
    public Vector3 openRotation = new Vector3(0f, 0f, -90f);
    
    [Tooltip("How many seconds it takes to open.")]
    public float openDuration = 1.5f;

    private bool isOpen = false;

    // This is the public function you will call from the Keypad event
    public void OpenLid()
    {
        if (!isOpen && lidTransform != null)
        {
            StartCoroutine(AnimateLidRoutine());
        }
    }

    private IEnumerator AnimateLidRoutine()
    {
        isOpen = true;
        float elapsedTime = 0f;
        
        // Convert Euler angles to Quaternions for smooth, gimbal-lock-free interpolation
        Quaternion startingRot = Quaternion.Euler(closedRotation);
        Quaternion targetRot = Quaternion.Euler(openRotation);

        while (elapsedTime < openDuration)
        {
            // Slerp (Spherical Linear Interpolation) creates a smooth rotational arc
            lidTransform.localRotation = Quaternion.Slerp(startingRot, targetRot, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the exact final rotation to ensure precision at the end of the animation
        lidTransform.localRotation = targetRot;
    }
}