using UnityEngine;
using System.Collections.Generic;

public class DrillManager : MonoBehaviour
{
    public static DrillManager Instance;

    [System.Serializable]
    public class DrillData
    {
        public int slotIndex;
        public int speedLevel;
        public int depthLevel;
    }

    public List<DrillData> drills = new List<DrillData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveDrill(int slotIndex, int speed, int depth)
    {
        var existing = drills.Find(d => d.slotIndex == slotIndex);
        if (existing != null)
        {
            existing.speedLevel = speed;
            existing.depthLevel = depth;
        }
        else
        {
            drills.Add(new DrillData
            {
                slotIndex = slotIndex,
                speedLevel = speed,
                depthLevel = depth
            });
        }
    }

    public DrillData GetDrill(int slotIndex)
    {
        return drills.Find(d => d.slotIndex == slotIndex);
    }
}
