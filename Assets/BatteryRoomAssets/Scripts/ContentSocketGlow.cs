using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ContentSocketGlow : MonoBehaviour
{
    public Renderer cupRender;
    public Color glowColor = Color.green;
    public ParticleSystem cupParticle;
    public EscapeManager escapeManager;
    public XRGrabInteractable content;

    public Vector3 insertOffset;

    public void OnContentInserted()
    {
        cupRender.material.EnableKeyword("_EMISSION");
        cupRender.material.SetColor("_EmissionColor", glowColor);
        cupParticle.Play();

        content.enabled = false;
        Rigidbody rb = content.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        content.transform.position = transform.position + insertOffset;
        content.transform.rotation = transform.rotation;

        if (escapeManager != null) escapeManager.MarkContentComplete();
    }
}