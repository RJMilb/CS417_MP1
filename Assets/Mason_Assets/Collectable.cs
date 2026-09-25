using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Collectible : MonoBehaviour
{
    public ProgressManager progressManager;

    private bool collected = false;

    public void Collect(SelectEnterEventArgs args)
    {
        if (collected)
            return;

        collected = true;

        progressManager.CollectItem();

        Destroy(gameObject);
    }
}