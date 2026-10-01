using UnityEngine;

using TMPro;

public class FlowerPuzzleManager : MonoBehaviour
{
    [Header("Scene References")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor[] pots = new UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor[5]; 
    public TextMeshProUGUI tvScreenText;
    public TextMeshProUGUI podiumText; 
    
    [Header("Puzzle Settings")]
    public string safeCode = "48151";

    private bool phaseOneComplete = false;
    private bool phaseTwoComplete = false;

    private readonly string[] phaseOneGoal = { "TulipRed", "TulipYellow", "TulipTeal", "TulipBlue", "TulipViolet" };
    private readonly string[] phaseTwoGoal = { "TulipBlue", "TulipViolet", "TulipTeal", "TulipRed", "TulipYellow" };

    // Triggered by the Socket Interactors every time a flower is placed
    public void OnFlowerPlacedOrRemoved()
    {
        if (phaseTwoComplete) return;

        if (!phaseOneComplete)
        {
            if (CheckOrder(phaseOneGoal))
            {
                phaseOneComplete = true;
                podiumText.text = "The screen on the back wall will reveal a code once the two correct flower orders are achieved.\n1/2";
                RevealNumbers();
            }
        }
        else 
        {
            if (CheckOrder(phaseTwoGoal))
            {
                phaseTwoComplete = true;
                podiumText.text = "The screen on the back wall will reveal a code once the two correct flower orders are achieved.\n2/2";
                tvScreenText.text = safeCode;
            }
        }
    }

    private bool CheckOrder(string[] goalSequence)
    {
        for (int i = 0; i < pots.Length; i++)
        {
            if (!pots[i].hasSelection) return false;

            // XRI 2.0+ uses interactablesSelected[0] to grab the current object in the socket
            string placedObjectName = pots[i].interactablesSelected[0].transform.name;
            
            if (placedObjectName != goalSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    private void RevealNumbers()
    {
        foreach (var pot in pots)
        {
            Flower flower = pot.interactablesSelected[0].transform.GetComponent<Flower>();
            if (flower != null)
            {
                flower.ShowNumber();
            }
        }
    }
}