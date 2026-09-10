using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = 0.2f;

    public float maxDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.localPosition.magnitude;
        float safeDistance = Mathf.Max(distance, 0.5f);

        Vector3 acceleration = -gravity * transform.localPosition / Mathf.Pow(safeDistance, 3);
        velocity += acceleration * Time.deltaTime;
        transform.localPosition += velocity * Time.deltaTime;


        if (distance >= maxDistance && distance > 0.001f)
        {
            Vector3 direction = transform.localPosition.normalized;
            transform.position = direction * maxDistance;
            velocity = -velocity * 0.8f;
        }
    }
}
