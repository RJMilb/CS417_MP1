using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class USBTerminalController : MonoBehaviour
{
    public Light pointLight;
    public GardenRoomManager roomManager;
    public string validItemName = "USB-Stick";

    public void OnSocketEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.name == validItemName)
        {
            pointLight.intensity = 100.0f;
            pointLight.color = Color.white;
            if (roomManager != null)
            {
                roomManager.StartTimer();
            }
        }
    }
}