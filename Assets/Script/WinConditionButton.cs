using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionButton : MonoBehaviour
{
    public AsteroidSlotManager asteroidSlotManager;
    public string victorySceneName = "VictoryScene";
    public GameObject winConditionGUI;

    public void CheckWinCondition()
    {
        // Check all drills placed
        int totalSlots = asteroidSlotManager.slots.Count;
        int placedDrills = DrillManager.Instance.drills.Count;

        if (placedDrills < totalSlots)
        {
            ShowWinConditionGUI();
            return;
        }

        // Check all drills maxed
        foreach (var drill in DrillManager.Instance.drills)
        {
            if (drill.speedLevel < 3 || drill.depthLevel < 3)
            {
                ShowWinConditionGUI();
                return;
            }
        }

        // Check ship level
        int shipLevel = PlayerPrefs.GetInt("ShipLevel", 1);
        if (shipLevel < 5)
        {
            ShowWinConditionGUI();
            return;
        }

        // Check credits
        if (CurrencyManager.Instance.currency < 100000)
        {
            ShowWinConditionGUI();
            return;
        }

        // If all conditions are met
SceneManager.LoadScene(4);
    }

    void ShowWinConditionGUI()
    {
        if (winConditionGUI != null)
        {
            winConditionGUI.SetActive(true);
        }
        else
        {
            Debug.Log("Win conditions not met, but no GUI assigned.");
        }
    }
}