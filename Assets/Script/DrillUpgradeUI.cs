using UnityEngine;
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
        speedLevelText.text = $"SPEED LEVEL: {currentDrill.level}";
        speedLevelText.fontSize = 25;
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
        int cost = selectedUpgrade == UpgradeType.Speed
            ? currentDrill.speedUpgradeCost
            : currentDrill.depthUpgradeCost;

        upgradeButtonText.text = $"UPGRADE ({cost})";
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
