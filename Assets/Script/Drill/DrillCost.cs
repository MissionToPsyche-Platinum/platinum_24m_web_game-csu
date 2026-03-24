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
    public int GetNextDrillCost()
    {
        return Mathf.RoundToInt(baseDrillCost * Mathf.Pow(drillCostMultiplier, drillsPlaced)); // Cost = (baseDrillCost) * (drillCostMultiplier) ^ (drillsPlaced)
    }
    public void RegisterDrill()
    {
        drillsPlaced++;

    }
}
