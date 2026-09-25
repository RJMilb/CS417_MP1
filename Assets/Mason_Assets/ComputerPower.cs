using UnityEngine;

public class ComputerPower : MonoBehaviour
{
    public GameObject blackScreen;

    public void TurnOn()
    {
        blackScreen.SetActive(false);
    }
}
