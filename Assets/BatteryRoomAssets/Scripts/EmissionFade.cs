using UnityEngine;
using System.Collections;

public class EmissionFade : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color glowColor = Color.white;
    public float fadeDuration = 0.5f;

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startIntensity, float endIntensity)
    {
        targetRenderer.material.EnableKeyword("_EMISSION");
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            float intensity = Mathf.Lerp(startIntensity, endIntensity, t);
            targetRenderer.material.SetColor("_EmissionColor", glowColor * intensity);
            yield return null;
        }

        targetRenderer.material.SetColor("_EmissionColor", glowColor * endIntensity);

        if (endIntensity <= 0f)
        {
            targetRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}