using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsOrbit : MonoBehaviour
{
    [Header("Orbit Target")]
    [SerializeField] private Rigidbody targetBody;

    [Header("Physics Settings")]
    [Tooltip("Adjusts the strength of gravity")]
    [SerializeField] private float gravitationalConstant = 100f;

    private Rigidbody myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        if (targetBody != null)
        {
            SetInitialOrbitalVelocity();
        }
    }

    public void Initialize(Rigidbody target)
    {
        targetBody = target;

        if (targetBody != null)
        {
            SetInitialOrbitalVelocity();
        }
    }

    void FixedUpdate()
    {
        if (targetBody != null)
        {
            ApplyGravitationalPull();
        }
    }

    private void SetInitialOrbitalVelocity()
    {
        // 1. Find the vector pointing from this object to the target
        Vector3 directionToTarget = targetBody.position - transform.position;
        float distance = directionToTarget.magnitude;

        if (distance == 0f) return;

        // 2. Calculate the required orbital speed for a stable circular orbit
        float orbitalSpeed = Mathf.Sqrt((gravitationalConstant * targetBody.mass) / distance);

        // 3. Project the controller's forward direction onto the orbital plane.
        // This removes any accidental 'away' or 'toward' movement, keeping the orbit stable.
        Vector3 normalToTarget = directionToTarget.normalized;
        Vector3 forwardDirection = transform.forward;

        // Project forward onto the plane perpendicular to the target
        Vector3 stableTangent = Vector3.ProjectOnPlane(forwardDirection, normalToTarget).normalized;

        // If the player points directly at the center, ProjectOnPlane becomes zero. 
        // Fall back to a default cross product so the physics don't break.
        if (stableTangent == Vector3.zero)
        {
            stableTangent = Vector3.Cross(normalToTarget, transform.up).normalized;
        }

        // 4. Inject the initial velocity directly into the Rigidbody
        myBody.linearVelocity = stableTangent * orbitalSpeed;
    }

    private void ApplyGravitationalPull()
    {
        // 1. Calculate directional data
        Vector3 direction = targetBody.position - transform.position;
        float distance = direction.magnitude;

        // Prevent division by zero if they overlap perfectly
        if (distance == 0f) return;

        // 2. Newton's Law of Universal Gravitation: F = G * (m1 * m2) / r^2
        float forceMagnitude = gravitationalConstant * (myBody.mass * targetBody.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        // 3. Apply the force to the physics engine
        myBody.AddForce(force, ForceMode.Force);
    }
}
