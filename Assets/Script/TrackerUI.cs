using UnityEngine;
using TMPro; // Use TextMeshPro for modern UI text

//2. ScoreUI.cs (Handles the UI Display):
//Attach this script to a GameObject with a UI Text component (using TextMeshPro is recommended). 


public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        // Subscribe to the OnScoreChanged event
        ScoreManager.OnScoreChanged += UpdateScoreText;
        ScoreManager.IronChanged += UpdateScoreText;
        ScoreManager.NickelChanged += UpdateScoreText;
    }

    void OnDisable()
    {
        // Unsubscribe from the event to prevent memory leaks
        ScoreManager.OnScoreChanged -= UpdateScoreText;
    }

    void Start()
    {
        // Initialize the UI text when the game starts
        UpdateScoreText(ScoreManager.Instance.Score);
        UpdateScoreText(ScoreManager.Instance.Iron);
        UpdateScoreText(ScoreManager.Instance.Nickel);
    }

    void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore.ToString();
        }
    }
}

