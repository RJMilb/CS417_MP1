using TMPro;
using UnityEngine;

public class LossTimer : MonoBehaviour
{
    public float startTime;
    public TextMeshProUGUI timeText;
    public GameObject failPopup;

    private float currentTime;
    private bool isRunning = true;


    void Start()
    {
        currentTime = startTime;
        if (failPopup != null) failPopup.SetActive(false);

    }

    void Update()
    {
        if (!isRunning) return;
        
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            TimeUp();
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timeText.text = "Time Left: " + minutes + ":" + seconds.ToString("00");
    }

    void TimeUp()
    {
        if (failPopup != null) failPopup.SetActive(true);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
