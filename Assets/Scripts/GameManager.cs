using UnityEngine;
using TMPro; // or UnityEngine.UI for standard text

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText; // Assign your UI TextMeshPro here

    [Header("Timer")]
    public float gameDuration = 60f; // 60 seconds
    public TMP_Text timerText;        // Assign a TextMeshPro for countdown
    private float remainingTime;
    private bool isGameActive = true;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        remainingTime = gameDuration;
        UpdateScoreUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        // Countdown logic
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isGameActive = false; // Stop the game
        }

        UpdateTimerUI();
    }

    // Call this to add points
    public void AddScore(int amount)
    {
        if (!isGameActive) return; // Do not add points if time is up

        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(remainingTime); // Round up for display
            timerText.text = $"Time: {seconds}s";
        }
    }
}
