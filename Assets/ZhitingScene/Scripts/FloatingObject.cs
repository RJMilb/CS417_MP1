using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatHeight = 0.2f; 
    public float floatSpeed = 1f;    
    public float rotateSpeed = 20f;  

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
