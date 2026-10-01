using UnityEngine;
using TMPro;

public class SprinklerController : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public GardenRoomManager roomManager;
    
    private int outputLevel = 0;
    private bool isFixed = false;

    public void IncrementLevel()
    {
        outputLevel = Mathf.Clamp(outputLevel + 1, 0, 100);
        UpdateTextDisplay();
        CheckGoal();
    }

    public void DecrementLevel()
    {
        outputLevel = Mathf.Clamp(outputLevel - 1, 0, 100);
        UpdateTextDisplay();
    }

    private void UpdateTextDisplay()
    {
        outputText.text = $"Sprinkler Output: {outputLevel}\n+           -";
    }

    private void CheckGoal()
    {
        if (outputLevel >= 25)
        {
            isFixed = true;
            if (roomManager != null)
            {
                roomManager.SprinklerFixed();
            }
        }
    }
}