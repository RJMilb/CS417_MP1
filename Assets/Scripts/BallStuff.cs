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

        // 2. Calculate the required orbital speed for a stable circular orbit: v = sqrt((G * M) / r)
        // Note: Using targetBody.mass assuming its mass dictates the pull strength
        float orbitalSpeed = Mathf.Sqrt((gravitationalConstant * targetBody.mass) / distance);

        // 3. Find a perpendicular vector (tangent) along the orbit plane
        // We use Vector3.up as our orbital plane normal
        Vector3 tangentDirection = Vector3.Cross(directionToTarget.normalized, transform.forward).normalized;

        // 4. Inject the initial velocity directly into the Rigidbody
        myBody.linearVelocity = tangentDirection * orbitalSpeed;
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
