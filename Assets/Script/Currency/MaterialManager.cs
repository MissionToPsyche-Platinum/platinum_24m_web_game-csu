using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    public int materials;
    public bool canGenerate = true; 

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            materials = 0; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void AddMaterial(int amount)
    {
        if (!canGenerate) return;
        materials += amount;
    }

    
    public bool SpendMaterial(int amount)
    {
        if (materials < amount)
            return false;

        materials -= amount;
        return true;
    }

    
    public void ClearMaterial()
    {
        materials = 0;
    }
}