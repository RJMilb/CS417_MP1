using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GardenRoomManager : MonoBehaviour
{
    [Header("UI & Timer")]
    public TextMeshProUGUI timerText;
    private float timeLeft = 60f;
    private bool timerActive = false;
    private bool sprinklerFixed = false;

    [Header("Puzzle Items")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable[] flowers = new UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable[5];

    void Start()
    {
        // Lock flowers at startup to prevent interaction
        foreach (var flower in flowers)
        {
            if (flower != null) flower.enabled = false;
        }
        
        if (timerText != null) timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerActive && !sprinklerFixed)
        {
            timeLeft -= Time.deltaTime;
            
            // Update UI
            int seconds = Mathf.CeilToInt(timeLeft);
            timerText.text = $"Time Remaining for Sprinkler: {seconds}s";

            if (timeLeft <= 0)
            {
                timerActive = false;
                ResetRoom();
            }
        }
    }

    public void StartTimer()
    {
        if (!sprinklerFixed)
        {
            timerActive = true;
            if (timerText != null) timerText.gameObject.SetActive(true);
        }
    }

    public void SprinklerFixed()
    {
        sprinklerFixed = true;
        timerActive = false;
        if (timerText != null) timerText.text = "Sprinkler Fixed!";
        
        // Unlock flowers so the player can arrange them
        foreach (var flower in flowers)
        {
            if (flower != null) flower.enabled = true;
        }
    }

    private void ResetRoom()
    {
        // Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}