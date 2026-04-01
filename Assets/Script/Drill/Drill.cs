using UnityEngine;

public class Drill : MonoBehaviour
{
    // Drill Levels
    public int speedLevel = 1;      // affects generation interval
    public int depthLevel = 1;      // affects min/max material
    public int maxLevel = 3;        // Max level for both speed and depth

    // Upgrade Cost
    public int speedUpgradeCost = 1000;
    public int depthUpgradeCost = 1500;

    // Placement cost
    public int placementCost = 250;

    // Mining Stats
    public float generationInterval;  // seconds between mining
    int minMaterial;
    int maxMaterial;

    float timer;

    // Drill visuals
    public SpriteRenderer drillRenderer;
    public Sprite goldenDrillSprite;

    // Animator  animate golden drill later
    public Animator drillAnimator;             

    void Start()
    {
        ApplySpeedStats();
        ApplyDepthStats();
        placementCost = DrillCost.Instance.GetNextDrillCost();
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
    }

    void GenerateMaterial()
    {
        int minedAmount = Random.Range(minMaterial, maxMaterial + 1);
        MaterialManager.Instance.AddMaterial(minedAmount);
    }

    public void UpgradeSpeed()
    {
        if (speedLevel >= maxLevel) return; 
        if (!CurrencyManager.Instance.SpendCurrency(speedUpgradeCost)) return;

        speedLevel++;
        ApplySpeedStats();
        speedUpgradeCost += 1000;
        CheckForMaxLevelVisuals();
    }

    public void UpgradeDepth()
    {
        if (depthLevel >= maxLevel) return; 
        if (!CurrencyManager.Instance.SpendCurrency(depthUpgradeCost)) return;

        depthLevel++;
        ApplyDepthStats();
        depthUpgradeCost += 1500;
        CheckForMaxLevelVisuals();
    }

    // Stats calculations
    void ApplySpeedStats()
    {
        // Speed upgrades reduce the generation interval
        switch (speedLevel)
        {
            case 1: generationInterval = 3f; break;
            case 2: generationInterval = 2f; break;
            case 3: generationInterval = 1f; break;
        }
    }

    void ApplyDepthStats()
    {
        // Depth upgrades increase min/max material mined per tick
        switch (depthLevel)
        {
            case 1: minMaterial = 1; maxMaterial = 7; break;
            case 2: minMaterial = 5; maxMaterial = 15; break;
            case 3: minMaterial = 10; maxMaterial = 20; break;
        }
    }

    void CheckForMaxLevelVisuals()
    {
        // Only switch visuals if BOTH levels are max
        if (speedLevel >= maxLevel && depthLevel >= maxLevel)
        {
            if (drillRenderer != null && goldenDrillSprite != null)
            {
                drillRenderer.sprite = goldenDrillSprite;
            }

            // Optional: enable animation if assigned
            if (drillAnimator != null)
            {
                drillAnimator.SetTrigger("Golden"); // you can set up an animator trigger
            }
        }
    }
}

