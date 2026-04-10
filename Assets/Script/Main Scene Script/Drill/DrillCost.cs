using UnityEngine;

public class DrillCost : MonoBehaviour
{
    public static DrillCost Instance;
    public int drillsPlaced = 0;
    public int baseDrillCost = 250;
    public float drillCostMultiplier = 3f; //3 times

    void Awake()
    {
        Instance = this;
    }

    public int GetCostForSlot(int slotIndex)
    {
        return Mathf.RoundToInt(baseDrillCost * Mathf.Pow(drillCostMultiplier, slotIndex)); ; // Cost = (baseDrillCost) * (drillCostMultiplier) ^ (drillsPlaced)
    }
}
