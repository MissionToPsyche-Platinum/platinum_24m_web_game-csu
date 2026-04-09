using UnityEngine;
using UnityEngine.UI;
using TMPro; //mohammed added for text mesh pro support
public class DrillSlot : MonoBehaviour
{
   
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

        if (drillPrefab != null && drillSpawnPoint != null)
        {
            var drillObj = Instantiate(drillPrefab, drillSpawnPoint.position, Quaternion.identity);
            Drill drillComp = drillObj.GetComponent<Drill>();

            // Save initial drill stats
            DrillManager.Instance.SaveDrill(
                slotManager.slots.IndexOf(this),
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

