using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public Slider keyProgressBar;
    public Slider collectibleProgressBar;

    public TMP_Text keyProgressText;
    public TMP_Text collectibleProgressText;

    private int keysCompleted = 0;
    private int collectiblesFound = 0;

    private const int totalKeys = 3;
    private const int totalCollectibles = 3;

    void Start()
    {
        keyProgressBar.minValue = 0;
        keyProgressBar.maxValue = totalKeys;

        collectibleProgressBar.minValue = 0;
        collectibleProgressBar.maxValue = totalCollectibles;

        UpdateUI();
    }

    public void CompleteKey()
    {
        if (keysCompleted >= totalKeys)
            return;

        keysCompleted++;
        UpdateUI();
    }

    public void CollectItem()
    {
        if (collectiblesFound >= totalCollectibles)
            return;

        collectiblesFound++;
        UpdateUI();
    }

    void UpdateUI()
    {
        keyProgressBar.value = keysCompleted;
        collectibleProgressBar.value = collectiblesFound;

        keyProgressText.text =
            "Keys: " + keysCompleted + " / " + totalKeys;

        collectibleProgressText.text =
            "Collectibles: " + collectiblesFound + " / " + totalCollectibles;
    }
}
