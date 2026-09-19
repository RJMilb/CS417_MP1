
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectShooter : MonoBehaviour
{
    public InputActionReference shootAction;

    public GameObject projectilePrefab;
    public Transform controller;
    public Transform attractor;

    public float gravity = 0.2f;
    public float spawnDistance = 0.3f;

    void OnEnable()
    {
        if (shootAction == null) return;

        shootAction.action.Enable();
        shootAction.action.performed += Shoot;
    }

    void OnDisable()
    {
        if (shootAction == null) return;

        shootAction.action.performed -= Shoot;
    }

    void Shoot(InputAction.CallbackContext ctx)
    {
        if (projectilePrefab == null ||
            controller == null ||
            attractor == null)
        {
            Debug.LogWarning("ObjectShooter has missing references!");
            return;
        }

        Vector3 spawnPosition =
            controller.position + controller.forward * spawnDistance;

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,
            controller.rotation
        );

        CometOrbit orbit = projectile.GetComponent<CometOrbit>();

        if (orbit == null)
        {
            Debug.LogWarning("Projectile has no CometOrbit script!");
            return;
        }

        orbit.attractor = attractor;
        orbit.gravity = gravity;

        Vector3 radial = spawnPosition - attractor.position;

        float distance = radial.magnitude;

        if (distance < 0.001f)
        {
            Debug.LogWarning("Projectile spawned at the attractor!");
            Destroy(projectile);
            return;
        }

        Vector3 direction = controller.forward;

        direction = Vector3.ProjectOnPlane(direction, radial);

        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = Vector3.Cross(radial, controller.up);
        }

        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = Vector3.Cross(radial, Vector3.up);
        }

        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = Vector3.Cross(radial, Vector3.right);
        }

        direction.Normalize();

        float orbitSpeed = Mathf.Sqrt(gravity / distance);

        orbit.velocity = direction * orbitSpeed;
    }
}
