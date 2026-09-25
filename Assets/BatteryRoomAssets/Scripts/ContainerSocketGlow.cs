using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ContainerSocketGlow : MonoBehaviour
{
    public Renderer cupRender;
    public Color glowColor = Color.green;
    public ParticleSystem cupParticle;
    public EscapeManager escapeManager;
    public XRGrabInteractable container;
    public float fadeDuration = 0.3f;

    public Vector3 insertOffset;

    public void OnContainerInserted()
    {

        StartCoroutine(FadeInGlow());
        cupParticle.Play();

        container.enabled = false;
        Rigidbody rb = container.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        container.transform.position = transform.position + insertOffset;
        container.transform.rotation = transform.rotation;

        if (escapeManager != null) escapeManager.MarkContainerComplete();
    }

    IEnumerator FadeInGlow()
    {
        cupRender.material.EnableKeyword("_EMISSION");
        float elapsed = 0f;

        while(elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            cupRender.material.SetColor("_EmissionColor", glowColor * t);
            yield return null;
        }
        cupRender.material.SetColor("_EmissionColor", glowColor);
    }
}