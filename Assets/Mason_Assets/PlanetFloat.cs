using UnityEngine;

public class PlanetFloat : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 20f, 0f);
    private bool floating = true;

    void Update()
    {
        if (!floating)
            return;

        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void StopFloating()
    {
        floating = false;
    }
}