using UnityEngine;
using TMPro;

public class CollectibleSystem : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;
    private int collectedCount = 0;


    public void CollectOne()
    {
        collectedCount++;
        collectibleText.text = "Collected: " + collectedCount;
    }
}
