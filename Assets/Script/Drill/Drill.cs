using UnityEngine;

public class Drill : MonoBehaviour
{
    //Drill Levels
    public int speedLevel = 1;      // affects generation interval
    public int depthLevel = 1;      // affects min/max material

    //Upgrade Cost
    public int speedUpgradeCost = 1000;
    public int depthUpgradeCost = 1500;

    //Placement cost
    public int placementCost = 250;

    //Mining Stats
    public float generationInterval;  // seconds between mining
    int minMaterial;
    int maxMaterial;

    float timer;

    void Start()
    {
        ApplySpeedStats();
        ApplyDepthStats();
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
        if (!CurrencyManager.Instance.SpendCurrency(speedUpgradeCost))
            return;

        speedLevel++;
        ApplySpeedStats();
        speedUpgradeCost += 1000;
    }

    public void UpgradeDepth()
    {
        if (!CurrencyManager.Instance.SpendCurrency(depthUpgradeCost))
            return;

        depthLevel++;
        ApplyDepthStats();
        depthUpgradeCost += 1500;
    }

    // ---------------------------
    // Stats calculations
    // ---------------------------
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
}

