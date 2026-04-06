using UnityEngine;

public class MiningSceneSetup : MonoBehaviour
{
    void Start()
    {
        if (MaterialManager.Instance != null)
            MaterialManager.Instance.canGenerate = true;
    }
}