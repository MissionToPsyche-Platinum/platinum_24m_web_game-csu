using UnityEngine;
using System.Collections.Generic;

public class AsteroidSlotManager : MonoBehaviour
{
    public List<DrillSlot> slots;

    private void Start()
    {
        RestoreDrills();
        ActivateNextSlotInLine();
    }

    private void RestoreDrills()
    {
        if (DrillManager.Instance == null)
        {
            Debug.LogWarning("DrillManager instance not found!");
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            var data = DrillManager.Instance.GetDrill(i);

            if (data != null)
            {
                // Slot is occupied because a drill exists
                slots[i].occupied = true;
                slots[i].gameObject.SetActive(false); // hide slot since drill exists

                if (slots[i].drillPrefab != null && slots[i].drillSpawnPoint != null)
                {
                    var drillObj = Instantiate(slots[i].drillPrefab, slots[i].drillSpawnPoint.position, Quaternion.identity);
                    Drill drillComp = drillObj.GetComponent<Drill>();
                    drillComp.speedLevel = data.speedLevel;
                    drillComp.depthLevel = data.depthLevel;
                }
            }
            else
            {
                // Slot is empty
                slots[i].occupied = false;
                slots[i].gameObject.SetActive(false); // hide by default
            }
        }
    }

    private void ActivateNextSlotInLine()
    {
        // Activate only the next unoccupied slot in sequence
        foreach (var slot in slots)
        {
            if (!slot.occupied)
            {
                slot.gameObject.SetActive(true);
                break; // only one slot is active at a time
            }
        }
    }

    public void OnSlotUsed(DrillSlot usedSlot)
    {
        int index = slots.IndexOf(usedSlot);

        if (index >= 0 && index + 1 < slots.Count)
        {
            // Activate the next slot in sequence after placement
            slots[index + 1].gameObject.SetActive(true);
        }
    }
}