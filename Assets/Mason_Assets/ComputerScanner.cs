
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ComputerScanner : MonoBehaviour
{
    public GameObject correctKeycard;
    public TMP_Text statusText;

    private bool authenticated = false;

    public void ScanCard(SelectEnterEventArgs args)
    {
        if (authenticated) return;

        GameObject card = args.interactableObject.transform.gameObject;

        if (card == correctKeycard)
        {
            authenticated = true;
            statusText.text = "Access Granted\nComputer Unlocked";
        }
        else
        {
            statusText.text = "Access Denied\nIncorrect Keycard\nNeeds Purple Key Card";
        }
    }
}