using UnityEngine;

public class Flower : MonoBehaviour
{
    public Canvas numberCanvas;

    void Start()
    {
        if (numberCanvas != null)
        {
            numberCanvas.gameObject.SetActive(false);
        }
    }

    public void ShowNumber()
    {
        if (numberCanvas != null)
        {
            numberCanvas.gameObject.SetActive(true);
        }
    }
}