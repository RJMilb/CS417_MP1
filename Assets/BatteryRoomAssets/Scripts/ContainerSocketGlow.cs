using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ContainerSocketGlow : MonoBehaviour
{
    public Renderer cupRender;
    public Color glowColor = Color.green;
    public ParticleSystem cupParticle;
    public EscapeManager escapeManager;
    public XRGrabInteractable container;

    public Vector3 insertOffset;

    public void OnContainerInserted()
    {
        cupRender.material.EnableKeyword("_EMISSION");
        cupRender.material.SetColor("_EmissionColor", glowColor);
        cupParticle.Play();

        container.enabled = false;
        Rigidbody rb = container.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        container.transform.position = transform.position + insertOffset;
        container.transform.rotation = transform.rotation;

        if (escapeManager != null) escapeManager.MarkContainerComplete();
    }
}