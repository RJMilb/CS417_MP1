using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private int fuseCount = 0;
    [SerializeField] private Animator myAnimator;

    void Update()
    {
        if(fuseCount > 3)
        {
            myAnimator.SetBool("Open", true);
        }
    }

    public void countUp()
    {
        fuseCount++;
    }

    public void countDown()
    {
        fuseCount--;
    }
}
