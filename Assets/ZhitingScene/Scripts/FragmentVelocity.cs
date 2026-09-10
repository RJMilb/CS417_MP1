using UnityEngine;

public class FragmentVelocity : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
