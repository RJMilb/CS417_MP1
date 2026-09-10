using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public AudioSource canvasAudio;
    public ParticleSystem greenLight;
    public ParticleSystem yellowLight;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasAudio.Play();
            greenLight.Play();
            yellowLight.Play();
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasAudio.Stop();
            greenLight.Stop();
            yellowLight.Stop();
        }
    }
}
