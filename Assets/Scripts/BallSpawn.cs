using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public InputActionReference action;
    public Rigidbody centralGravityTarget;
    public AudioSource audioSource;
    public ParticleSystem effectToPlay;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);

            // Get the physics component and inject the target!
            PhysicsOrbit orbitScript = ball.GetComponent<PhysicsOrbit>();
            if (orbitScript != null)
            {
                orbitScript.Initialize(centralGravityTarget);
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (effectToPlay != null)
            {
                effectToPlay.Play(); 
            }
        };
    }
}