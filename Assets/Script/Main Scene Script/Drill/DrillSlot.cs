using UnityEngine;
using UnityEngine.UI;
using TMPro; //mohammed added for text mesh pro support
public class DrillSlot : MonoBehaviour
{
    public Drill currentDrill;
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
        Debug.Log($"[BuildDrill] CLICKED SLOT: {gameObject.name}");

        if (drillPrefab != null && drillSpawnPoint != null)
        {
            var drillObj = Instantiate(drillPrefab, drillSpawnPoint.position, Quaternion.identity);
            Drill drillComp = drillObj.GetComponent<Drill>();

            // Assign the slot index
            drillComp.slotIndex = slotManager.slots.IndexOf(this);
            Debug.Log($"Clicked slot object: {gameObject.name}");
            Debug.Log($"[BuildDrill] SLOT MARKED OCCUPIED: {gameObject.name}");
            Debug.Log($"[BuildDrill] slotIndex ASSIGNED: {drillComp.slotIndex} | SLOT NAME: {gameObject.name}");

            // Restore saved levels if they exist
            var savedData = DrillManager.Instance.GetDrill(drillComp.slotIndex);
            if (savedData != null)
            {
                drillComp.speedLevel = savedData.speedLevel;
                drillComp.depthLevel = savedData.depthLevel;
            }


            // Save initial drill stats to DrillManager
            DrillManager.Instance.SaveDrill(
                drillComp.slotIndex,
                drillComp.speedLevel,
                drillComp.depthLevel
            );
        }

        gameObject.SetActive(false);

        if (slotManager != null)
        {
            slotManager.OnSlotUsed(this);
        }
    }
}

