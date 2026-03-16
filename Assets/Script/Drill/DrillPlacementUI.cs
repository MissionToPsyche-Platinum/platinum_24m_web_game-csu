using UnityEngine;
using UnityEngine.UI; //mohammed added for text mesh pro support
using TMPro;

public class DrillUIPanel : MonoBehaviour
{
    private DrillSlot currentSlot;

    [Header("UI Elements")]
    public TMP_Text costText; // Assign this in the Inspector to show the placement cost
    public GameObject buyButton; // Optional: can disable if player can't afford

    // Called by the slot when clicked
    public void Open(DrillSlot slot)
    {
        currentSlot = slot;

        if (currentSlot != null && currentSlot.drillPrefab != null)
        {
            int cost = DrillCost.Instance.GetNextDrillCost();

            if (costText != null)
                costText.text = $"Would you like to place a drill here for {cost} Credits";

            // Optional: grey out buy button if player can't afford
            if (buyButton != null)
                buyButton.SetActive(CurrencyManager.Instance.currency >= cost);
        }

        gameObject.SetActive(true);
    }

    public void OnBuyClicked()
    {
        if (currentSlot == null || currentSlot.drillPrefab == null)
            return;

        int cost = DrillCost.Instance.GetNextDrillCost();

        // Check if player can afford
        if (CurrencyManager.Instance.currency < cost)
        {
            Debug.Log("Not enough credits to place drill!");
            return;
        }

        // Build drill
        currentSlot.BuildDrill();

        // Deduct correct cost
        CurrencyManager.Instance.SpendCurrency(cost);

        // Register drill for next cost increase
        DrillCost.Instance.RegisterDrill();

        Close();
    }

    public void OnCancelClicked()
    {
        Close();
    }

    private void Close()
    {
        currentSlot = null;
        gameObject.SetActive(false);
    }
}




