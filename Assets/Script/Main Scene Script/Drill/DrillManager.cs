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
        public float timer; 
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
    private float timer = 0f;

void Update()
{
    timer += Time.deltaTime;
    
    foreach (var drill in drills)
    {
        drill.timer += Time.deltaTime;
        float interval = GetIntervalForSpeed(drill.speedLevel);
        
        if (drill.timer >= interval)
        {
            GenerateMaterials(drill);
            drill.timer = 0f;
        }
    }
}

void GenerateMaterials(DrillData drill)
{
    int min = 1, max = 4;
    switch (drill.depthLevel)
    {
        case 2: min = 2; max = 6; break;
        case 3: min = 5; max = 10; break;
    }
    int amount = Random.Range(min, max + 1);
    MaterialManager.Instance.AddMaterial(amount);
}

float GetIntervalForSpeed(int speedLevel)
{
    switch (speedLevel)
    {
        case 2: return 2.5f;
        case 3: return 2.0f;
        default: return 3f;
    }
}




}
