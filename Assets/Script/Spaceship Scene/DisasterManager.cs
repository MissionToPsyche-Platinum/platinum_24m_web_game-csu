using System.Collections;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public static DisasterManager Instance;

    [Header("Disaster Settings")]
    public float minInterval = 10f; // 5 min
    public float maxInterval = 15f; // 7 min
    public int repairCost = 500;

    public bool disasterActive = false;

    public bool hasDebt = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(DisasterLoop());
    }

    IEnumerator DisasterLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            if (!disasterActive)
                TriggerDisaster();
        }
    }

    void TriggerDisaster()
    {
        disasterActive = true;
        repairCost = Random.Range(200, 800); // randomize cost each time
        DisasterUI.Instance.ShowDisasterPopup(repairCost);
    }

    public void ResolveDisaster()
    {
        disasterActive = false;
    }
}