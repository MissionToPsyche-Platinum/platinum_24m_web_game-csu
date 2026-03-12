using UnityEngine;
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
            int cost = currentSlot.drillPrefab.GetComponent<Drill>().placementCost;
            if (costText != null)
                costText.text = $"Cost: {cost} Credits";

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

        Drill drill = currentSlot.drillPrefab.GetComponent<Drill>();

        // Check if the player can afford the drill
        if (CurrencyManager.Instance.currency < drill.placementCost)
        {
            Debug.Log("Not enough credits to place drill!");
            return;
        }

        // Deduct the placement cost
        CurrencyManager.Instance.SpendCurrency(drill.placementCost);

        // Build the drill in this slot
        currentSlot.BuildDrill();

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




