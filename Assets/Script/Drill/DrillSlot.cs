using UnityEngine;
using UnityEngine.UI;
using TMPro; //mohammed added for text mesh pro support
public class DrillSlot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool occupied = false;
    public GameObject drillPrefab;
    public Transform drillSpawnPoint;
    public AsteroidSlotManager slotManager;
    public DrillUIPanel drillUI;
    public Button slotButton;

    private void Awake()
    {
        if (slotButton != null)
        {
            // Assign the click listener dynamically at runtime
            slotButton.onClick.AddListener(OnSlotClick);
        }
        else
        {
            Debug.LogWarning("SlotButton not assigned on " + gameObject.name);
        }
    }

    public void OnSlotClick()
    {
        if (occupied) return;

        // Open the UI panel for this slot
        if (drillUI != null)
        {
            drillUI.Open(this);
        }
        else
        {
            Debug.LogWarning("DrillUI not assigned for " + gameObject.name);
        }
    }

    public void BuildDrill()
    {
        if (occupied) return;

        occupied = true;

        // Spawn the drill prefab at the designated spawn point
        if (drillPrefab != null && drillSpawnPoint != null)
        {
            Instantiate(drillPrefab, drillSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("DrillPrefab or SpawnPoint not assigned for " + gameObject.name);
        }

        // Hide this slot button now that the drill is built
        gameObject.SetActive(false);

        // Notify the slot manager to unlock the next slot
        if (slotManager != null)
        {
            slotManager.OnSlotUsed(this);
        }
    }
}

