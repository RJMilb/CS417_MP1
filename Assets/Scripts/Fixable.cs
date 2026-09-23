using UnityEngine;
using UnityEngine.Events;

public class Fixable : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy"; // The tag of the specific object
    public UnityEvent onSpecificCollision; // The event that will fire

    public Collider me;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OUCH! I just hit: " + other.gameObject.name);
        if (other.CompareTag(targetTag))
        {
            onSpecificCollision.Invoke();
        }
    }

}
