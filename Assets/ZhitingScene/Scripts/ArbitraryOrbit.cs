using UnityEngine;

public class ArbitraryOrbit : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = 0.2f;

    public Transform attractor;

    public float maxDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - attractor.position).magnitude;
        float safeDistance = Mathf.Max(distance, 0.5f);


        Vector3 acceleration = -gravity * (transform.position - attractor.position) / Mathf.Pow(safeDistance, 3);
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;


        if (distance >= maxDistance && distance > 0.001f)
        {
            Vector3 direction = (transform.position - attractor.position).normalized;
            Debug.Log("边界拉回触发,当前distance: " + distance + ", 计算出的方向: " + direction);
            transform.position = attractor.position + (direction * maxDistance);
            velocity = -velocity * 0.8f;
        }

    }
}
