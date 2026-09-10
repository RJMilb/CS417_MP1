using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float degPerS = 0.0f;
    void Update()
    {
        if (degPerS != 0)
        {
            transform.Rotate(0, degPerS * Time.deltaTime, 0);
        }
    }
}
