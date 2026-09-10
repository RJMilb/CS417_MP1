using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public RiftMachine riftMachine;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fragment"))
        {
            Destroy(other.gameObject);
            riftMachine.RegisterHit();
            Destroy(gameObject);
        }

    }
}
