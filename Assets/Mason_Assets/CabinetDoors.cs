
using UnityEngine;
using System.Collections;

using UnityEngine.InputSystem;

public class CabinetDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openDuration = 1.5f;

    public Vector3 leftMovement = new Vector3(-0.5f, 0, 0);
    public Vector3 rightMovement = new Vector3(0.5f, 0, 0);

    private bool opened = false;

    public void OpenDoors()
    {
        if (opened) return;

        opened = true;
        StartCoroutine(AnimateDoors());
    }

    IEnumerator AnimateDoors()
    {
        Vector3 leftStartScale = leftDoor.localScale;
        Vector3 rightStartScale = rightDoor.localScale;

        Vector3 leftStartPos = leftDoor.localPosition;
        Vector3 rightStartPos = rightDoor.localPosition;

        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / openDuration);

            // Smooth animation
            t = t * t * (3f - 2f * t);

            // Shrink both doors
            leftDoor.localScale = new Vector3(
                Mathf.Lerp(leftStartScale.x, leftStartScale.x * 0.01f, t),
                leftStartScale.y,
                leftStartScale.z
            );

            rightDoor.localScale = new Vector3(
                Mathf.Lerp(rightStartScale.x, rightStartScale.x * 0.01f, t),
                rightStartScale.y,
                rightStartScale.z
            );

            // Move each door independently
            leftDoor.localPosition =
                leftStartPos + leftMovement * t;

            rightDoor.localPosition =
                rightStartPos + rightMovement * t;

            yield return null;
        }
    }

void Update()
{
    if (Keyboard.current != null &&
        Keyboard.current.oKey.wasPressedThisFrame)
    {
        Debug.Log("O key pressed!");
        OpenDoors();
    }
}
}