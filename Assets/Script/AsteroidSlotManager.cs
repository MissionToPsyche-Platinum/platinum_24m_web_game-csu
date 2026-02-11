using UnityEngine;
using System.Collections.Generic;

public class AsteroidSlotManager : MonoBehaviour
{
    public List<DrillSlot> slots;

    private void Start()
    {
        // Ensure only the first slot is active at start
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(i == 0);
        }
    }

    public void OnSlotUsed(DrillSlot usedSlot)
    {
        int index = slots.IndexOf(usedSlot);
        Debug.Log($"Used slot: {usedSlot.name}, index: {index}");

        if (index >= 0 && index + 1 < slots.Count)
        {
            slots[index + 1].gameObject.SetActive(true);
            Debug.Log($"Unlocked slot: {slots[index + 1].name}");
        }
    }

}


