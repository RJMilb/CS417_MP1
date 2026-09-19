
using UnityEngine;
using System;

public class CometOrbit : MonoBehaviour
{
    public Transform attractor;
    public Vector3 velocity;

    public float gravity = 0.2f;
    public int speed = 1;

    void Update()
    {
        if (attractor == null) return;

        for (int i = 0; i < speed; i++)
        {
            SimulateStep(Time.deltaTime);
        }
    }

    void SimulateStep(float dt)
    {
        Vector3 position = transform.position - attractor.position;

        double distance = Math.Sqrt(
            Math.Pow(position.x, 2) +
            Math.Pow(position.y, 2) +
            Math.Pow(position.z, 2)
        );

        if (distance < 0.001) return;

        double ax = -gravity * position.x / Math.Pow(distance, 3);
        double ay = -gravity * position.y / Math.Pow(distance, 3);
        double az = -gravity * position.z / Math.Pow(distance, 3);

        velocity.x += (float)(ax * dt);
        velocity.y += (float)(ay * dt);
        velocity.z += (float)(az * dt);

        transform.position += velocity * dt;
    }
}