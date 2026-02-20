using UnityEngine;

public class Drill : MonoBehaviour
{
    public int level = 1;

    // Speed upgrade
    public float generationInterval = 4f; // seconds
    public int materialPerTick = 3;

    // Depth / range upgrade
    public int depthLevel = 1;

    // Upgrade costs 
    public int speedUpgradeCost = 10;
    public int depthUpgradeCost = 15;

    public void UpgradeSpeed()
    {
        if (!CanAfford(speedUpgradeCost)) return;

        SpendResources(speedUpgradeCost);
        level++;
        generationInterval *= 0.9f; 
        materialPerTick += 1;
        speedUpgradeCost += 10;
    }

    public void UpgradeDepth()
    {
        if (!CanAfford(depthUpgradeCost)) return;
        SpendResources(depthUpgradeCost);
        depthLevel++;
        depthUpgradeCost += 15;
    }

    bool CanAfford(int cost)
    {
        return true;
    }

    void SpendResources(int cost)
    {
    }
}

