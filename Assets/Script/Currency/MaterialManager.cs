using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    public int materials;

    void Start()
    {
        Instance = this;
        materials = 0;
    }

    public void AddMaterial(int amount)
    {
        materials += amount;

    }
    public void ClearMaterial()
    {
        materials = 0;
    }
}
