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

    // By default, the speed upgrade is selected.
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
        // Display SPEED LEVEL
        if (currentDrill.speedLevel >= currentDrill.maxLevel)
        {
            speedLevelText.text = "SPEED LEVEL: MAXED";
        }
        else
        {
            speedLevelText.text = $"SPEED LEVEL: {currentDrill.speedLevel}";
        }
        speedLevelText.fontSize = 25;

        // Display DEPTH LEVEL
        if (currentDrill.depthLevel >= currentDrill.maxLevel)
        {
            depthLevelText.text = "DEPTH LEVEL: MAXED";
        }
        else
        {
            depthLevelText.text = $"DEPTH LEVEL: {currentDrill.depthLevel}";
        }
        depthLevelText.fontSize = 25;

        // Update upgrade button
        UpdateUpgradeButton();
    }

    // Select Speed Function/Button
    public void SelectSpeed()
    {
        selectedUpgrade = UpgradeType.Speed;
        UpdateUpgradeButton();
    }

    // Select Depth Function/Button
    public void SelectDepth()
    {
        selectedUpgrade = UpgradeType.Depth;
        UpdateUpgradeButton();
    }

    // Update upgrade button cost when upgrade type changes
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
                cost = currentDrill.speedUpgradeCost;
            }
        }
        else // Depth upgrade selected
        {
            if (currentDrill.depthLevel >= currentDrill.maxLevel)
            {
                isMaxed = true;
            }
            else
            {
                cost = currentDrill.depthUpgradeCost;
            }
        }

        // Update the button text
        upgradeButtonText.text = isMaxed ? "MAXED" : $"{cost}";
    }

    // Upgrade Confirm button
    public void OnUpgradePressed()
    {
        if (selectedUpgrade == UpgradeType.Speed)
            currentDrill.UpgradeSpeed();
        else
            currentDrill.UpgradeDepth();

        Refresh();
    }
}
