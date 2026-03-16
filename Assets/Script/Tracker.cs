using UnityEngine;
using System; // Required for the Action event

public class ScoreManager : MonoBehaviour
{
    // Event to notify other scripts when the score changes
    public static event Action<int> OnScoreChanged;
    public static event Action<int> IronChanged;
    public static event Action<int> NickelChanged;

    // The current score
    private int score = 0;
    private int iron_ore = 0;
    private int nickel_ore = 0;

    // Public property to access the score from other scripts (read-only from outside)
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            // When the score changes, invoke the event
            OnScoreChanged?.Invoke(score);
        }
    }
    public int Iron
    {
        get { return iron_ore; }
        private set
        {
            iron_ore = value;
            // When the score changes, invoke the event
            IronChanged?.Invoke(iron_ore);
        }
    }

    public int Nickel
    {
        get { return nickel_ore; }
        private set
        {
            nickel_ore = value;
            // When the score changes, invoke the event
            NickelChanged?.Invoke(nickel_ore);
        }
    }



    // Method to add points
    public void AddPoints(int pointsToAdd)
    {
        if (pointsToAdd > 0)
        {
            Score += pointsToAdd;
        }
    }

    public void AddIron(int pointsToAdd)
    {
        if (pointsToAdd > 0)
        {
            iron_ore += pointsToAdd;
        }
    }
    public void AddNickel(int pointsToAdd)
    {
        if (pointsToAdd > 0)
        {
            nickel_ore += pointsToAdd;
        }
    }

    // Use a singleton pattern if you want this manager to persist across scenes
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    private static IronManager instance;
    public static IronManager Instance { get { return instance; } }

    private static NickelManager instance;
    public static NickelManager Instance { get { return instance; } }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want score to persist across levels
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public class Tracker
{
    
}
