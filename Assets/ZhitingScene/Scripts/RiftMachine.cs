using UnityEngine;

public class RiftMachine : MonoBehaviour
{
 public Light blueLight;
    public GameObject earth;

    public ParticleSystem yellowEffect;
    private int hitCount = 0;

    public void RegisterHit()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            Debug.Log("Machine Start");
            blueLight.intensity = 50f;
            Instantiate(earth, transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
            yellowEffect.Play();
        }
    }

}