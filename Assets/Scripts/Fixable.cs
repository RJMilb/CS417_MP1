using UnityEngine;
using UnityEngine.Events;

public class Fixable : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy"; // The tag of the specific object
    public UnityEvent onSpecificCollision, onHit; // The event that will fire
    public int hits = 1;
    private int hits_recived = 0;
    public Collider me;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OUCH! I just hit: " + other.gameObject.name);
        if (other.CompareTag(targetTag))
        {
            hits_recived++;
            onHit.Invoke();
            //onSpecificCollision.Invoke();
        }
    }

    void Update()
    {
        if(hits_recived >= hits)
        {
            onSpecificCollision.Invoke();
            this.enabled = false;
        }
    }

}
