using UnityEngine;

public class LockProgress : MonoBehaviour
{
    public ProgressManager progressManager;

    private bool completed = false;

    public void CompleteLock()
    {
        if (completed)
            return;

        completed = true;
        progressManager.CompleteKey();
    }
}