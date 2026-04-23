using UnityEngine;
//This script is attached to each drill in the game. It handles the drill's speed and depth levels, generates materials over time , and
//manages the upgrade system for both speed and depth. It also interacts with the DrillManager to save and load drill data across scenes.

public class Drill : MonoBehaviour
{
    public int slotIndex;         
    public int speedLevel = 1;
    public int depthLevel = 1;
    public int maxLevel = 3;
    public int placementCost = 250;
    public float generationInterval;
    int minMaterial;
    int maxMaterial;
    float timer;
    public SpriteRenderer drillRenderer;
    public Sprite goldenDrillSprite;
    public Animator drillAnimator;

    void Start()
    {
        ApplySpeedStats();
        ApplyDepthStats();

        placementCost = DrillCost.Instance.GetCostForSlot(slotIndex);
        CheckForMaxLevelVisuals();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= generationInterval)
        {
            GenerateMaterial();
            timer = 0f;
        }

        CheckForMaxLevelVisuals();
    }

    void GenerateMaterial()
    {
        int minedAmount = Random.Range(minMaterial, maxMaterial + 1);
        MaterialManager.Instance.AddMaterial(minedAmount);
    }

    
    // UPGRADE SPEED
    
    public void UpgradeSpeed()
    {
        if (speedLevel >= maxLevel) return;

        int cost = GetSpeedUpgradeCost();
        if (!CurrencyManager.Instance.SpendCurrency(cost)) return;

        speedLevel++;

        ApplySpeedStats();
        DrillManager.Instance.SaveDrill(slotIndex, speedLevel, depthLevel);

        CheckForMaxLevelVisuals();
    }

    
    // UPGRADE DEPTH
    
    public void UpgradeDepth()
    {
        if (depthLevel >= maxLevel) return;

        int cost = GetDepthUpgradeCost();
        if (!CurrencyManager.Instance.SpendCurrency(cost)) return;

        depthLevel++;

        ApplyDepthStats();
        DrillManager.Instance.SaveDrill(slotIndex, speedLevel, depthLevel);

        CheckForMaxLevelVisuals();
    }

    
    // COST CALCULATION 
    
    public int GetSpeedUpgradeCost()
    {
        return Mathf.RoundToInt(
            1000f *
            Mathf.Pow(1.50f, speedLevel - 1) *
            (1f + slotIndex * 0.95f)
        );
    }

    public int GetDepthUpgradeCost()
    {
        return Mathf.RoundToInt(
            1500f *
            Mathf.Pow(1.75f, depthLevel - 1) *
            (1f + slotIndex * 0.95f)
        );
    }

    
    // STATS
    
    void ApplySpeedStats()
    {
        switch (speedLevel)
        {
            case 1: generationInterval = 3f; break;
            case 2: generationInterval = 2.5f; break;
            case 3: generationInterval = 2.0f; break;
        }
    }

    void ApplyDepthStats()
    {
        switch (depthLevel)
        {
            case 1: minMaterial = 1; maxMaterial = 4; break;
            case 2: minMaterial = 2; maxMaterial = 6; break;
            case 3: minMaterial = 5; maxMaterial = 10; break;
        }
    }

    void CheckForMaxLevelVisuals()
    {
        if (speedLevel >= maxLevel && depthLevel >= maxLevel)
        {
            if (drillAnimator != null && !drillAnimator.GetBool("Golden"))
            {
                drillAnimator.SetBool("Golden", true);
            }
        }
    }
}