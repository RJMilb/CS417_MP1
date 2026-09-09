using UnityEngine;
using System;

public class CometOrbit : MonoBehaviour
{
    public Vector3 velocity;
    public float speed = 1f;

    void Update()
    {
        const double gravity = 50;

        float dt = Time.deltaTime * speed;

        Vector3 position = transform.position - new Vector3(0, 4, 0);

        double distance = Math.Sqrt(
            Math.Pow(position.x, 2) +
            Math.Pow(position.y, 2) +
            Math.Pow(position.z, 2)
        );

        double ax = -gravity * position.x / Math.Pow(distance, 3);
        double ay = -gravity * position.y / Math.Pow(distance, 3);
        double az = -gravity * position.z / Math.Pow(distance, 3);

        velocity.x = velocity.x + (float)(ax * dt);
        velocity.y = velocity.y + (float)(ay * dt);
        velocity.z = velocity.z + (float)(az * dt);

        transform.position = transform.position + velocity * dt;
    }
}