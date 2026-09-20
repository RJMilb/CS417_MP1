using UnityEngine;

public class ContainerSocketGlow : MonoBehaviour
{
    public Renderer cupRender;
    public Color glowColor = Color.green;

    public ParticleSystem cupParticle;

    public void OnContainerInserted()
    {
        cupRender.material.EnableKeyword("_EMISSION");
        cupRender.material.SetColor("_EmissionColor", glowColor);
        cupParticle.Play();

    }

    public void OnContainerRemoved()
    {
        cupRender.material.DisableKeyword("_EMISSION");
        cupParticle.Stop();
    }
}
