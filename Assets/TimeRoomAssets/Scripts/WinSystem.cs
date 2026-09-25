using UnityEngine;
using System.Collections;
using TMPro;

public class WinSystem : MonoBehaviour
{
    public bool batteryInserted = false;
    public bool fingerPrint = false;
    public bool tool = false;
    public bool timeCode = false;

    public Light winLight;
    public float lightFadeDuration;
    public float lightTargetIntensity;

    public GameObject winText;
    public Transform timeModel;
    public Vector3 moveOffset;

    public float duration = 2f;
    private bool hasWon = false;

    public Light ceilingSpotlight;
    public Color[] cycleColors = new Color[] { Color.red, Color.blue, Color.yellow, Color.green };
    public float colorHoldDuration;



    void Start()
    {
        if (winText != null) winText.SetActive(false);

    }

    public void MarkBatteryInserted()
    {
        batteryInserted = true;
        CheckWin();
    }

    void CheckWin()
    {
        if (hasWon) return;
        if (batteryInserted) //&& fingerPrint && tool && timeCode
        {
            hasWon = true;
            StartCoroutine(PlayWinSequence());
            StartCoroutine(FadeInLight());
            StartCoroutine(CycleSpotlightColors());
        }
    }

    IEnumerator PlayWinSequence()
    {
        Vector3 modelStart = timeModel.position;
        Vector3 modelEnd = modelStart + new Vector3(moveOffset.x, moveOffset.y, moveOffset.z);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            timeModel.position = Vector3.Lerp(modelStart, modelEnd, t);
            yield return null;
        }

        if (winText != null) winText.SetActive(true);
    }

    IEnumerator FadeInLight()
    {
        float startIntensity = winLight.intensity;
        float elapsed = 0f;

        while(elapsed < lightFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lightFadeDuration;
            winLight.intensity = Mathf.Lerp(startIntensity, lightTargetIntensity, t);
            yield return null;
        }

        winLight.intensity = lightTargetIntensity;
    }

    IEnumerator CycleSpotlightColors()
    {
        if (ceilingSpotlight == null) yield break;
        for (int loop = 0; loop < 2; loop++)
        {
            foreach(Color c in cycleColors)
            {
                ceilingSpotlight.color = c;
                yield return new WaitForSeconds(colorHoldDuration);
            }
        }
    }

}
