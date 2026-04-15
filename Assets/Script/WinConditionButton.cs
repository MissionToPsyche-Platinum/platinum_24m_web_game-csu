using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinConditionButton : MonoBehaviour
{
    public AsteroidSlotManager asteroidSlotManager;

    public string victorySceneName = "VictoryScene";
    public GameObject winConditionGUI;

    public Image drillProgressBox;
    public Image shipLevelBox;
    public Image creditsBox;

    public Button winButton;

    public Sprite metSprite;
    public Sprite notMetSprite;

    [System.Serializable]
    public class WinState
    {
        public bool drillComplete;
        public bool shipMet;
        public bool creditsMet;
    }

    private static WinState cachedState = new WinState();

    public void CheckWinCondition()
    {
        // DRILL PROGRESS (only updates if asteroid system exists in this scene)
        if (asteroidSlotManager != null && DrillManager.Instance != null)
        {
            bool drillComplete = false;

            int totalSlots = asteroidSlotManager.slots.Count;
            int placedDrills = DrillManager.Instance.drills.Count;

            if (placedDrills >= totalSlots)
            {
                drillComplete = true;

                // check all drills maxed
                foreach (var drill in DrillManager.Instance.drills)
                {
                    if (drill.speedLevel < 3 || drill.depthLevel < 3)
                    {
                        drillComplete = false;
                        break;
                    }
                }
            }

            cachedState.drillComplete = drillComplete;
        }

        // SHIP LEVEL (cached, only changes when you upgrade)
        int shipLevel = PlayerPrefs.GetInt("ShipLevel", 1);
        cachedState.shipMet = shipLevel >= 5;

        // CREDITS (ALWAYS LIVE)
        cachedState.creditsMet = CurrencyManager.Instance.currency >= 100000;

        // UPDATE UI
        SetBox(drillProgressBox, cachedState.drillComplete);
        SetBox(shipLevelBox, cachedState.shipMet);
        SetBox(creditsBox, cachedState.creditsMet);

        // SHOW UI
        if (winConditionGUI != null)
            winConditionGUI.SetActive(true);

        // ENABLE WIN BUTTON ONLY IF ALL CONDITIONS MET
        if (winButton != null)
        {
            winButton.interactable =
                cachedState.drillComplete &&
                cachedState.shipMet &&
                cachedState.creditsMet;
        }
    }

    public void ConfirmWin()
    {
        if (cachedState.drillComplete &&
            cachedState.shipMet &&
            cachedState.creditsMet)
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }

    private void SetBox(Image box, bool met)
    {
        if (box != null)
        {
            box.sprite = met ? metSprite : notMetSprite;
        }
    }
}