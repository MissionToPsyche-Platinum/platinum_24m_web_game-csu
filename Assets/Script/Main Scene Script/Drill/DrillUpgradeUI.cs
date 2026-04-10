using UnityEngine;
using UnityEngine.UI; //mohammed added for text mesh pro support
using TMPro;

public class DrillUpgradeUI : MonoBehaviour
{
    public static DrillUpgradeUI Instance;

    public TMP_Text speedLevelText;
    public TMP_Text depthLevelText;
    public TMP_Text upgradeButtonText;

    Drill currentDrill;

    enum UpgradeType { Speed, Depth }
    UpgradeType selectedUpgrade;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Open(Drill drill)
    {
        currentDrill = drill;
        selectedUpgrade = UpgradeType.Speed;
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        currentDrill = null;
    }

    void Refresh()
    {
        // SPEED
        if (currentDrill.speedLevel >= currentDrill.maxLevel)
            speedLevelText.text = "SPEED LEVEL: MAXED";
        else
            speedLevelText.text = $"SPEED LEVEL: {currentDrill.speedLevel}";

        speedLevelText.fontSize = 25;

        // DEPTH
        if (currentDrill.depthLevel >= currentDrill.maxLevel)
            depthLevelText.text = "DEPTH LEVEL: MAXED";
        else
            depthLevelText.text = $"DEPTH LEVEL: {currentDrill.depthLevel}";

        depthLevelText.fontSize = 25;

        UpdateUpgradeButton();
    }

    public void SelectSpeed()
    {
        selectedUpgrade = UpgradeType.Speed;
        UpdateUpgradeButton();
    }

    public void SelectDepth()
    {
        selectedUpgrade = UpgradeType.Depth;
        UpdateUpgradeButton();
    }

    void UpdateUpgradeButton()
    {
        bool isMaxed = false;
        int cost = 0;

        if (selectedUpgrade == UpgradeType.Speed)
        {
            if (currentDrill.speedLevel >= currentDrill.maxLevel)
            {
                isMaxed = true;
            }
            else
            {
                cost = currentDrill.GetSpeedUpgradeCost();
            }
        }
        else
        {
            if (currentDrill.depthLevel >= currentDrill.maxLevel)
            {
                isMaxed = true;
            }
            else
            {
                cost = currentDrill.GetDepthUpgradeCost();
            }
        }

        upgradeButtonText.text = isMaxed ? "MAXED" : $"{cost}";
    }

    public void OnUpgradePressed()
    {
        if (selectedUpgrade == UpgradeType.Speed)
            currentDrill.UpgradeSpeed();
        else
            currentDrill.UpgradeDepth();

        Refresh();
    }
}
