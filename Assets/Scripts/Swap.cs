using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swap : MonoBehaviour
{
    public InputActionReference action;

    public Vector3 position1;
    public Vector3 position2;

    public AudioSource audioSource;
    public ParticleSystem effectToPlay;
    private bool swap = true;

    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            WaitTime(3f);

            if (effectToPlay != null)
            {
                effectToPlay.Play();
            }

            if (swap)
            {
                transform.position = position1;
            }
            else
            {
                transform.position = position2;
            }

            swap = !swap;
        };
    }

    IEnumerator WaitTime(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
