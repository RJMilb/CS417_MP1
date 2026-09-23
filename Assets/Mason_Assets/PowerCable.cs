using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PowerCable : MonoBehaviour
{
    public Transform cableStart;
    public Transform cableEnd;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.useWorldSpace = true;
        line.startWidth = 0.015f;
        line.endWidth = 0.015f;
    }

    void LateUpdate()
    {
        if (cableStart == null || cableEnd == null)
            return;

        line.SetPosition(0, cableStart.position);
        line.SetPosition(1, cableEnd.position);
    }
}
