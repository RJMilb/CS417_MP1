using UnityEngine;

public class CollectibleObjects : MonoBehaviour
{
    public CollectibleSystem collectibleSystem;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        collectibleSystem.CollectOne();
        Destroy(gameObject);
    }


}
