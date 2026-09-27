using UnityEngine;
using System.Collections;

public class LeverSocketTrigger : MonoBehaviour
{
    public GameObject lightBeamObject;
    public GameObject fruitObject;
    public Transform mirrorTransform;
    public float mirrorTargetAngle = 45f;
    public float mirrorRotateDuration = 0.5f;
    public float lightBeamDuration = 0.5f;

    public void OnAssemblyComplete()
    {
        if (lightBeamObject != null) lightBeamObject.SetActive(false);
        if (fruitObject != null) fruitObject.SetActive(false);
        StartCoroutine(SequenceAfterInsert());
    }

    IEnumerator SequenceAfterInsert()
    {
        yield return StartCoroutine(RotateMirror(mirrorTargetAngle, mirrorRotateDuration));

        if (lightBeamObject != null) lightBeamObject.SetActive(true);

        yield return new WaitForSeconds(lightBeamDuration);

        if (fruitObject != null) fruitObject.SetActive(true);
    }

    IEnumerator RotateMirror(float targetAngle, float duration)
    {
        if (mirrorTransform == null) yield break;

        Quaternion startRotation = mirrorTransform.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            mirrorTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        mirrorTransform.localRotation = endRotation;
    }
}