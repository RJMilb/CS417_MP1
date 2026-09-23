using UnityEngine;
using System.Collections;
using TMPro;

public class EscapeManager : MonoBehaviour
{
    private bool containerComplete = false;
    private bool contentComplete = false;
    private bool handleComplete = false;

    public Transform containerTransform;
    public Transform contentTransform;
    public Vector3 containerInsideOffset;
    public Vector3 contentInsideOffset;
    public float suckInDuration = 1f;

    public GameObject batteryObject;
    public Transform batteryTransform;
    public Vector3 batteryOutsideOffset;
    public float batteryMoveDuration;

    public Transform lidTransform;
    public Vector3 lidSlideOffset;
    public float lidOpenDuration = 0.8f;

    private bool hasEscaped = false;

    private int score = 0;

    public TextMeshProUGUI scoreText;

    public LossTimer lossTimer;

    public void MarkContainerComplete()
    {
        containerComplete = true;
        score++;
        UpdateScore();
        CheckEscape();
    }

    public void MarkContentComplete()
    {
        contentComplete = true;
        score++;
        UpdateScore();
        CheckEscape();
    }

    public void MarkHandleComplete()
    {
        handleComplete = true;
        score++;
        UpdateScore();
        CheckEscape();
    }

    void UpdateScore()
    {
        scoreText.text = "Progress: " + score + "/3";
    }

    void CheckEscape()
    {
        if (hasEscaped) return;

        if (containerComplete && contentComplete && handleComplete)
        {
            hasEscaped = true;
            if (lossTimer != null) lossTimer.StopTimer();
            StartCoroutine(AssembleBattery());
        }
    }


    IEnumerator AssembleBattery()
    {
        StartCoroutine(MoveIn(containerTransform, containerInsideOffset, suckInDuration));
        StartCoroutine(MoveIn(contentTransform, contentInsideOffset, suckInDuration));
        yield return new WaitForSeconds(suckInDuration);

        if (containerTransform != null) containerTransform.gameObject.SetActive(false);
        if (contentTransform != null) contentTransform.gameObject.SetActive(false);

        yield return StartCoroutine(MoveIn(lidTransform, lidSlideOffset, lidOpenDuration));

        if (batteryObject != null)
        {
            batteryObject.SetActive(true);
            yield return StartCoroutine(MoveIn(batteryTransform, batteryOutsideOffset, batteryMoveDuration));

            Rigidbody batteryRb = batteryObject.GetComponent<Rigidbody>();
            Debug.Log("找到的Rigidbody是: " + batteryRb);
            if (batteryRb != null) batteryRb.isKinematic = false;
        }

        
    }

    IEnumerator MoveIn(Transform obj, Vector3 offset, float duration)
    {
        if (obj == null) yield break;

        Vector3 start = obj.position;
        Vector3 end = start + offset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            obj.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        obj.position = end;
    }

}