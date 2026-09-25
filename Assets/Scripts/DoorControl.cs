using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class DoorControl : MonoBehaviour
{
    private int fuseCount = 0;
    public UnityEvent onOpen, onFail;
    [SerializeField] private Animator myAnimator;

    public void OpenDoor() 
    {
        if (fuseCount > 3)
        {
            myAnimator.SetBool("Open", true);
            onOpen.Invoke();
        }
        else
        {
            onFail.Invoke();
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
