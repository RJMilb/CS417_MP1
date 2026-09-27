using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ContentSocketGlow : MonoBehaviour
{
    public Renderer cupRender;
    public Color glowColor = Color.green;
    public ParticleSystem cupParticle;
    public EscapeManager escapeManager;
    public XRGrabInteractable content;
    public float fadeDuration = 0.3f;
    public Vector3 insertOffset;

    public void OnContentInserted()
    {
        StartCoroutine(FadeInGlow());
        cupParticle.Play();

        content.enabled = false;
        Rigidbody rb = content.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        content.transform.position = transform.position + insertOffset;
        content.transform.rotation = transform.rotation;

        if (escapeManager != null) escapeManager.MarkContentComplete();
    }

    IEnumerator FadeInGlow()
    {
        cupRender.material.EnableKeyword("_EMISSION");
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            cupRender.material.SetColor("_EmissionColor", glowColor * t);
            yield return null;
        }
        cupRender.material.SetColor("_EmissionColor", glowColor);
    }

}